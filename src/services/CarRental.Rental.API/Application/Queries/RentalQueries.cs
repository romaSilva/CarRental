using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Rental.API.Application.Queries
{
    public class RentalQueries : IRentalQueries
    {
        public Task<IEnumerable<VehicleRentalDto>> GetRentals()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VehicleRentalDto>> GetRentalsByCustomer(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
