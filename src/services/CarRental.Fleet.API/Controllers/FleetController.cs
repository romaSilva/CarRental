using CarRental.Core.DomainObjects;
using CarRental.Fleet.API.Data;
using CarRental.Fleet.API.Models;
using CarRental.Fleet.API.ViewModels;
using CarRental.WebApi.Core.Controllers;
using CarRental.WebApi.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Fleet.API.Controllers
{
    public class FleetController : MainController
    {
        private readonly FleetContext _context;

        public FleetController(FleetContext context)
        {
            _context = context;
        }

        [HttpGet("vehicles")]
        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await _context.Vehicles.AsNoTracking().ToListAsync();
        }

        [HttpGet("vehicles/{vehicleId}")]
        public async Task<Vehicle> GetVehicle(Guid vehicleId)
        {
            return await _context.Vehicles.AsNoTracking().FirstOrDefaultAsync(v => v.Id == vehicleId);
        }

        [HttpPost("vehicles")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Operator)]
        public async Task<IActionResult> RegisterVehicle(VehicleViewModel vehicleViewModel)
        {
            var vehicle = new Vehicle(vehicleViewModel.PlateNumber, vehicleViewModel.Year, vehicleViewModel.HourValue, vehicleViewModel.BaggageSize,
                vehicleViewModel.Brand, vehicleViewModel.Model, vehicleViewModel.Category, vehicleViewModel.Fuel);

            _context.Vehicles.Add(vehicle);

            if (!await _context.Commit())
            {
                AddProcessingError("Something went wrong persisting the data");
                return CustomResponse();
            };

            return CustomResponse();
        }

        [HttpDelete("vehicles/{vehicleId}")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Operator)]
        public async Task<IActionResult> RemoveVehicle(Guid vehicleId)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                AddProcessingError("Vehicle not found");
                return CustomResponse();
            }

            _context.Vehicles.Remove(vehicle);

            if (!await _context.Commit())
            {
                AddProcessingError("Something went wrong persisting the data");
                return CustomResponse();
            };

            return CustomResponse();
        }
    }
}
