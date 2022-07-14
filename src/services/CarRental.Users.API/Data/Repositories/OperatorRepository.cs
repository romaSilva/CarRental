using CarRental.Core.Data;
using CarRental.Users.API.Models;

namespace CarRental.Users.API.Data.Repositories
{
    public class OperatorRepository : IOperatorRepository
    {
        private readonly UsersContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public OperatorRepository(UsersContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
