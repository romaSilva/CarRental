using CarRental.Core.DomainObjects;
using CarRental.Core.Mediator;
using CarRental.Rental.API.Application.Commands;
using CarRental.Rental.API.Application.Queries;
using CarRental.WebApi.Core.Controllers;
using CarRental.WebApi.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.Rental.API.Controllers
{
    public class RentalsController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IRentalQueries _rentalQueries;
        private readonly IIdentityUserService _identityUserService;

        public RentalsController(IMediatorHandler mediator, IRentalQueries rentalQueries, IIdentityUserService identityUserService)
        {
            _mediator = mediator;
            _rentalQueries = rentalQueries;
            _identityUserService = identityUserService;
        }

        [HttpGet]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Operator)]
        public async Task<IActionResult> ListRentals()
        {
            return CustomResponse(await _rentalQueries.GetRentals());
        }

        [HttpGet("customer-list")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Customer)]
        public async Task<IActionResult> ListRentalsByCustomer()
        {
            return CustomResponse(await _rentalQueries.GetRentalsByCustomer(_identityUserService.GetUserId()));
        }

        [HttpGet("in-progress")]
        public async Task<IActionResult> ListInProgressRentals()
        {
            return CustomResponse(await _rentalQueries.GetInProgressRentals());
        }

        [HttpPost("rent-vehicle")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Customer)]
        public async Task<IActionResult> RequestRental(RequestRentalCommand rentalRequest)
        {
            return CustomResponse(await _mediator.SendCommand(rentalRequest));
        }

        [HttpPost("add-inspection")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Operator)]
        public async Task<IActionResult> AddInspection(AddInspectionCommand addInspectionCommand)
        {
            return CustomResponse(await _mediator.SendCommand(addInspectionCommand));
        }
    }
}
