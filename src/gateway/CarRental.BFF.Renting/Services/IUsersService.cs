using CarRental.BFF.Renting.Models;
using System;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public interface IUsersService
    {
        Task<CustomerDto> GetCustomer(Guid userId);
    }
}