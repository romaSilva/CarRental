using CarRental.Core.DomainObjects;
using CarRental.Core.Messages.Integrations;
using CarRental.Users.API.Models;
using CarRental.Users.API.ViewModels;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Users.API.Application
{
    public class UsersAppService : BaseService, IUsersAppService
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly ICustomerRepository _customerRepository;

        public UsersAppService(IOperatorRepository operatorRepository, ICustomerRepository customerRepository)
        {
            _operatorRepository = operatorRepository;
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> AddAddress(Guid customerId, AddressViewModel addressViewModel)
        {
            var customer = await GetCustomer(customerId);

            if (customer == null)
            {
                AddErrorMessage("Customer not found");
                return ValidationResult;
            }

            var address = new Address(addressViewModel.ZipCode, addressViewModel.Street, addressViewModel.Number,
                                      addressViewModel.Complement, addressViewModel.City, addressViewModel.State, customerId);

            if (customer.Address != null)
            {
                _customerRepository.RemoveAddress(customer.Address);
            }

            customer.SetAddress(address);
            _customerRepository.AddAddress(address);

            _customerRepository.Update(customer);

            return await SaveChanges(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> CreateCustomer(CustomerRegisteredIntegrationEvent message)
        {
            var registeredCustomer = await _customerRepository.GetByCpf(message.Cpf);

            if (registeredCustomer != null)
            {
                AddErrorMessage("Customer already registered");
                return ValidationResult;
            }

            var customer = new Customer(message.Id, message.Name, message.Cpf, message.BirthDate);
            _customerRepository.Add(customer);

            return await SaveChanges(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> CreateOperator(OperatorRegisteredIntegrationEvent message)
        {
            var registeredOperator = await _operatorRepository.GetByCompanyRegistration(message.CompanyRegistration);

            if (registeredOperator != null)
            {
                AddErrorMessage("Operator already registered");
                return ValidationResult;
            }

            var newOperator = new Operator(message.Id, message.Name, message.CompanyRegistration);
            _operatorRepository.Add(newOperator);

            return await SaveChanges(_customerRepository.UnitOfWork);
        }

        public async Task<Customer> GetCustomer(Guid customerId)
        {
            return await _customerRepository.GetById(customerId);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerRepository.GetAll();
        }

        public async Task<Operator> GetOperator(Guid operatorId)
        {
            return await _operatorRepository.GetById(operatorId);
        }

        public async Task<IEnumerable<Operator>> GetOperators()
        {
            return await _operatorRepository.GetAll();
        }
    }
}
