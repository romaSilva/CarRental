using CarRental.Core.Data;
using CarRental.Rental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Rental.Data.Data.Repositories
{
    public class VehicleRentalRepository : IVehicleRentalRepository
    {
        private readonly RentalContext _context;

        public VehicleRentalRepository(RentalContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<VehicleRental>> GetByCustomer(Guid customerId)
        {
            return await _context.Rentals.Include(r => r.ReturnInspection).Where(r => r.CustomerId == customerId).ToListAsync();
        }

        public async Task<IEnumerable<VehicleRental>> GetAll()
        {
            return await _context.Rentals.AsNoTracking().Include(r => r.ReturnInspection).ToListAsync();
        }

        public async Task<IEnumerable<VehicleRental>> GetInProgressRentals()
        {
            return await _context.Rentals.Include(r => r.ReturnInspection)
                .Where(r => r.Status == RentalStatus.Located || r.Status == RentalStatus.Reserved).ToListAsync();
        }

        public async Task<VehicleRental> GetById(Guid rentalId)
        {
            return await _context.Rentals.Include(r => r.ReturnInspection).FirstOrDefaultAsync(r => r.Id == rentalId);
        }

        public void Add(VehicleRental vehicleRental)
        {
            _context.Rentals.Add(vehicleRental);
        }

        public void AddInspection(ReturnInspection inspection)
        {
            _context.Inspections.Add(inspection);
        }

        public void RemoveInspection(ReturnInspection returnInspection)
        {
            _context.Inspections.Remove(returnInspection);
        }

        public void Update(VehicleRental rental)
        {
            _context.Rentals.Update(rental);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
