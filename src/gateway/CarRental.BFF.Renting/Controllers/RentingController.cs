using CarRental.BFF.Renting.Models;
using CarRental.BFF.Renting.Services;
using CarRental.BFF.Renting.ViewModels;
using CarRental.Core.DomainObjects;
using CarRental.WebApi.Core.Controllers;
using CarRental.WebApi.Core.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CarRental.BFF.Renting.Controllers
{
    public class RentingController : MainController
    {
        private readonly IIdentityUserService _identityUserService;

        private readonly IFleetService _fleetService;
        private readonly IRentalsService _rentalsService;
        private readonly IUsersService _usersService;

        public RentingController(IFleetService fleetService, IRentalsService rentalService,
                                 IUsersService usersService, IIdentityUserService identityUserService)
        {
            _fleetService = fleetService;
            _rentalsService = rentalService;
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
            var vehiclesInCategory = await _fleetService.GetVehiclesByCategory(rentVehicleViewModel.Category);

            if (vehiclesInCategory == null)
            {
                AddProcessingError("Vehicle no longer available");
                return CustomResponse();
            }

            var unavailableRentals = await _rentalsService.GetRentalsInProgress();

            if (!AnyVehicleAvailable(vehiclesInCategory, unavailableRentals, out VehicleDto vehicle))
            {
                AddProcessingError("Vehicle no longer available");
                return CustomResponse();
            }

            var rentalRequest = CreateRentalRequest(customer, vehicle, rentVehicleViewModel);

            return CustomResponse(await _rentalsService.RequestRental(rentalRequest));
        }

        [HttpPost("return-inspection")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Operator)]
        public async Task<IActionResult> RegisterReturnInspection(ReturnInspectionViewModel returnInspectionViewModel)
        {
            returnInspectionViewModel.OperatorId = _identityUserService.GetUserId();
            return CustomResponse(await _rentalsService.AddInspection(returnInspectionViewModel));
        }

        private bool AnyVehicleAvailable(IEnumerable<VehicleDto> vehiclesInCategory, IEnumerable<RentalDto> unavailableRentals, out VehicleDto vehicle)
        {
            var unavailableIds = unavailableRentals.Select(r => r.VehicleId);
            vehicle = vehiclesInCategory.FirstOrDefault(v => !unavailableIds.Contains(v.Id));

            return vehicle != null;
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

