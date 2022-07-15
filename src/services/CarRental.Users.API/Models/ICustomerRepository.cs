using CarRental.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Users.API.Models
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetById(Guid customerId);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCpf(string cpf);
        void Add(Customer customer);
        void Update(Customer customer);
        void AddAddress(Address address);
        void UpdateAddress(Address address);
        void RemoveAddress(Address address);
    }
}
