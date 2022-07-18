using CarRental.BFF.Renting.Models;
using CarRental.BFF.Renting.Services;
using CarRental.BFF.Renting.ViewModels;
using CarRental.Core.DomainObjects;
using CarRental.WebApi.Core.Controllers;
using CarRental.WebApi.Core.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Controllers
{
    public class RentingController : MainController
    {
        private readonly IIdentityUserService _identityUserService;

        private readonly IFleetService _fleetService;
        private readonly IRentalService _rentalService;
        private readonly IUsersService _usersService;

        public RentingController(IFleetService fleetService, IRentalService rentalService,
                                 IUsersService usersService, IIdentityUserService identityUserService)
        {
            _fleetService = fleetService;
            _rentalService = rentalService;
            _usersService = usersService;
            _identityUserService = identityUserService;
        }

        [HttpPost("simulate-cost")]
        public async Task<IActionResult> SimulateRentalCost(SimulateRentalPriceViewModel simulateRentalPriceViewModel)
        {
            var vehicle = await _fleetService.GetVehicle(simulateRentalPriceViewModel.VehicleId);

            if (vehicle == null)
            {
                AddProcessingError("Vehicle no longer available");
                return CustomResponse();
            }

            var hours = (simulateRentalPriceViewModel.ReturnDate - simulateRentalPriceViewModel.RentDate).TotalHours;

            return CustomResponse(Math.Round(vehicle.HourValue * hours, 2));
        }

        [HttpPost("rent-vehicle")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Customer)]
        public async Task<IActionResult> RentVehicle(RentVehicleViewModel rentVehicleViewModel)
        {
            var userId = _identityUserService.GetUserId();

            var customer = await _usersService.GetCustomer(userId);
            var vehicle = await _fleetService.GetVehicle(rentVehicleViewModel.VehicleId);

            if (vehicle == null)
            {
                AddProcessingError("Vehicle no longer available");
                return CustomResponse();
            }

            var rentalRequest = CreateRentalRequest(customer, vehicle, rentVehicleViewModel);

            return CustomResponse(await _rentalService.RequestRental(rentalRequest));
        }

        [HttpPost("return-inspection")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Operator)]
        public async Task<IActionResult> RegisterReturnInspection(ReturnInspectionViewModel returnInspectionViewModel)
        {
            returnInspectionViewModel.OperatorId = _identityUserService.GetUserId();
            return CustomResponse(await _rentalService.AddInspection(returnInspectionViewModel));
        }

        private RentalDto CreateRentalRequest(CustomerDto customer, VehicleDto vehicle, RentVehicleViewModel rentVehicleViewModel)
        {
            return new RentalDto()
            {
                CustomerId = _identityUserService.GetUserId(),
                VehicleId = vehicle.Id,
                CustomerName = customer.Name,
                Cpf = customer.Cpf,
                RentDate = rentVehicleViewModel.RentDate,
                ReturnDate = rentVehicleViewModel.ReturnDate,
                PlateNumber = vehicle.PlateNumber,
                Model = vehicle.Model,
                Year = vehicle.Year,
                HourValue = vehicle.HourValue
            };
        }
    }
}

