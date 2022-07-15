using CarRental.Users.API.Application;
using CarRental.Users.API.Models;
using CarRental.Users.API.ViewModels;
using CarRental.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Users.API.Controllers
{
    public class UsersController : MainController
    {
        private readonly IUsersAppService _usersAppService;

        public UsersController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

        [HttpGet("operators")]
        public async Task<IEnumerable<Operator>> GetOperators()
        {
            return await _usersAppService.GetOperators();
        }

        [HttpGet("operators/{operatorId}")]
        public async Task<Operator> GetOperator(Guid operatorId)
        {
            return await _usersAppService.GetOperator(operatorId);
        }

        [HttpGet("customers")]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _usersAppService.GetCustomers();
        }

        [HttpGet("customers/{customerId}")]
        public async Task<Customer> GetCustomer(Guid customerId)
        {
            return await _usersAppService.GetCustomer(customerId);
        }

        [HttpPost("customers/add-address/{customerId}")]
        public async Task<IActionResult> AddAddress(Guid customerId, AddressViewModel address)
        {
            var result = await _usersAppService.AddAddress(customerId, address);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    AddProcessingError(error.ErrorMessage);
                }
            }

            return CustomResponse();
        }
    }
}
