using CarRental.BFF.Renting.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public interface IFleetService
    {
        Task<VehicleDto> GetVehicle(Guid vehicleId);
        Task<IEnumerable<VehicleDto>> GetVehiclesByCategory(Category category);
    }
}
