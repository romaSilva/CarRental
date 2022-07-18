using CarRental.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Rental.Domain.Models
{
    public interface IVehicleRentalRepository : IRepository<VehicleRental>
    {
        Task<VehicleRental> GetById(Guid rentalId);
        void Add(VehicleRental vehicleRental);
        void Update(VehicleRental rental);
        void AddInspection(ReturnInspection inspection);
        void RemoveInspection(ReturnInspection returnInspection);
    }
}
