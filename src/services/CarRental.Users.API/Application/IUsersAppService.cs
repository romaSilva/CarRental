using CarRental.Core.Messages.Integrations;
using CarRental.Users.API.Models;
using CarRental.Users.API.ViewModels;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Users.API.Application
{
    public interface IUsersAppService
    {
        public Task<Operator> GetOperator(Guid customerId);
        public Task<IEnumerable<Operator>> GetOperators();
        public Task<Customer> GetCustomer(Guid customerId);
        public Task<IEnumerable<Customer>> GetCustomers();
        public Task<ValidationResult> CreateOperator(OperatorRegisteredIntegrationEvent message);
        public Task<ValidationResult> CreateCustomer(CustomerRegisteredIntegrationEvent message);
        public Task<ValidationResult> AddAddress(Guid customerId, AddressViewModel address);
    }
}