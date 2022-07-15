using CarRental.Core.Data;
using CarRental.Users.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Customer> GetById(Guid customerId)
        {
            return await _context.Customers.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.Include(c => c.Address).AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetByCpf(string cpf)
        {
            return await _context.Customers.Include(c => c.Address).FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public void UpdateAddress(Address address)
        {
            _context.Addresses.Update(address);
        }

        public void RemoveAddress(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
