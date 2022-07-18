using CarRental.Rental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Rental.API.Application.Queries
{
    public interface IRentalQueries
    {
        Task<IEnumerable<VehicleRentalDto>> GetRentalsByCustomer(Guid customerId);
        Task<IEnumerable<VehicleRentalDto>> GetRentals();
        Task<IEnumerable<VehicleRentalDto>> GetInProgressRentals();
    }
}
