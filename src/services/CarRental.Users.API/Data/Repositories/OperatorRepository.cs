using CarRental.Core.Data;
using CarRental.Users.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Operator>> GetAll()
        {
            return await _context.Operators.AsNoTracking().ToListAsync();
        }

        public async Task<Operator> GetById(Guid operatorId)
        {
            return await _context.Operators.FindAsync(operatorId);
        }

        public async Task<Operator> GetByCompanyRegistration(string companyRegistration)
        {
            return await _context.Operators.FirstOrDefaultAsync(o => o.CompanyRegistration == companyRegistration);
        }

        public void Add(Operator newOperator)
        {
            _context.Operators.Add(newOperator);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
