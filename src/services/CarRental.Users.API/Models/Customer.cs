using CarRental.Core.DomainObjects;
using System;

namespace CarRental.Users.API.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Cpf Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Address Address { get; private set; }

        protected Customer() { }

        public Customer(Guid id, string name, string number, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Cpf = new Cpf(number);
            BirthDate = birthDate;
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }
    }
}
