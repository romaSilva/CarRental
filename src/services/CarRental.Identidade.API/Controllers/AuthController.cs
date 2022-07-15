using CarRental.Core.DomainObjects;
using CarRental.Core.Messages.Integrations;
using CarRental.Identidade.API.Models;
using CarRental.MessageBus;
using CarRental.WebApi.Core.Controllers;
using CarRental.WebApi.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Identidade.API.Controllers
{
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _sigInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;

        private readonly IMessageBus _bus;

        public AuthController(SignInManager<IdentityUser> sigInManager,
                              UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IOptions<AppSettings> appSettings,
                              IMessageBus bus)
        {
            _sigInManager = sigInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
            _bus = bus;
        }

        [HttpPost("register-operator")]
        public async Task<ActionResult> RegisterOperator(OperatorRegister operatorRegister)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = operatorRegister.CompanyRegistration,
                Email = operatorRegister.Email,
                EmailConfirmed = true
            };

            var result = await CreateUser(user, Constants.Roles.Operator, operatorRegister.Password);

            if (result.Succeeded)
            {
                var operatorResult = await CreateOperator(operatorRegister);

                if (!operatorResult.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);
                    return CustomResponse(operatorResult.ValidationResult);
                }

                return CustomResponse(await GenerateJwt(operatorRegister.CompanyRegistration));
            }

            foreach (var error in result.Errors)
            {
                AddProcessingError(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("register-customer")]
        public async Task<ActionResult> RegisterCustomer(CustomerRegister customerRegister)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = customerRegister.Cpf,
                Email = customerRegister.Email,
                EmailConfirmed = true
            };

            var result = await CreateUser(user, Constants.Roles.Customer, customerRegister.Password);

            if (result.Succeeded)
            {
                var customerResult = await CreateCustomer(customerRegister);

                if (!customerResult.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);
                    return CustomResponse(customerResult.ValidationResult);
                }

                return CustomResponse(await GenerateJwt(customerRegister.Cpf));
            }

            foreach (var error in result.Errors)
            {
                AddProcessingError(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _sigInManager.PasswordSignInAsync(userLogin.Username, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJwt(userLogin.Username));
            }

            if (result.IsLockedOut)
            {
                AddProcessingError("User blocked due to too many attempts");
            }

            AddProcessingError("Invalid username or password");
            return CustomResponse();
        }

        private async Task<ResponseMessage> CreateCustomer(CustomerRegister customerRegister)
        {
            var user = await _userManager.FindByEmailAsync(customerRegister.Email);

            var message = new CustomerRegisteredIntegrationEvent(Guid.Parse(user.Id), customerRegister.Name, customerRegister.Cpf, customerRegister.BirthDate);

            try
            {
                return await _bus.RequestAsync<CustomerRegisteredIntegrationEvent, ResponseMessage>(message);
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }

        private async Task<ResponseMessage> CreateOperator(OperatorRegister operatorRegister)
        {
            var user = await _userManager.FindByEmailAsync(operatorRegister.Email);

            var message = new OperatorRegisteredIntegrationEvent(Guid.Parse(user.Id), operatorRegister.Name, operatorRegister.CompanyRegistration);

            try
            {
                return await _bus.RequestAsync<OperatorRegisteredIntegrationEvent, ResponseMessage>(message);
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }

        private async Task<IdentityResult> CreateUser(IdentityUser user, string role, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return await AddToRole(user, role);
            }

            return result;
        }

        private async Task<IdentityResult> AddToRole(IdentityUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }

            return await _userManager.AddToRoleAsync(user, role);
        }

        private async Task<UserLoginResponse> GenerateJwt(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await AddUserClaims(claims, user);
            var encodedToken = EncodeToken(identityClaims);

            return GetLoginResponse(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> AddUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(Constants.Claims.Role, userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserLoginResponse GetLoginResponse(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UserLoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationInHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
