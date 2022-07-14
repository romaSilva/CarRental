using CarRental.Core.Data;
using CarRental.Users.API.Models;

namespace CarRental.Users.API.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly UsersContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CustomerRepository(UsersContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
