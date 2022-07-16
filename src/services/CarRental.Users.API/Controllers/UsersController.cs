using CarRental.Users.API.Application;
using CarRental.Users.API.Models;
using CarRental.Users.API.ViewModels;
using CarRental.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Users.API.Controllers
{
    public class UsersController : MainController
    {
        private readonly IUsersAppService _usersAppService;

        public UsersController(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
        }

        [HttpGet("operators")]
        public async Task<IEnumerable<Operator>> GetOperators()
        {
            return await _usersAppService.GetOperators();
        }

        [HttpGet("operators/{operatorId}")]
        public async Task<Operator> GetOperator(Guid operatorId)
        {
            return await _usersAppService.GetOperator(operatorId);
        }

        [HttpGet("customers")]
        public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
        {
            return MapToCustomerViewModel(await _usersAppService.GetCustomers());
        }

        [HttpGet("customers/{customerId}")]
        public async Task<CustomerViewModel> GetCustomer(Guid customerId)
        {
            return MapToCustomerViewModel(
                new List<Customer>() { await _usersAppService.GetCustomer(customerId) }).FirstOrDefault();
        }

        [HttpPost("customers/add-address/{customerId}")]
        public async Task<IActionResult> AddAddress(Guid customerId, AddressViewModel address)
        {
            var result = await _usersAppService.AddAddress(customerId, address);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    AddProcessingError(error.ErrorMessage);
                }
            }

            return CustomResponse();
        }

        private List<CustomerViewModel> MapToCustomerViewModel(IEnumerable<Customer> customers)
        {
            var customersViewModel = new List<CustomerViewModel>();

            foreach (var customer in customers)
            {
                customersViewModel.Add(new CustomerViewModel()
                {
                    Name = customer.Name,
                    Cpf = customer.Cpf.Number,
                    BirthDate = customer.BirthDate,
                    Address = customer.Address
                });
            }

            return customersViewModel;
        }
    }
}
