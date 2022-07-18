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
        Task<IEnumerable<VehicleRental>> GetByCustomer(Guid customerId);
        void Add(VehicleRental vehicleRental);
        void Update(VehicleRental rental);
        void AddInspection(ReturnInspection inspection);
        Task<IEnumerable<VehicleRental>> GetAll();
        void RemoveInspection(ReturnInspection returnInspection);
    }
}
