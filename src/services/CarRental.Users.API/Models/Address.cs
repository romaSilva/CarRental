using CarRental.Core.DomainObjects;
using System;

namespace CarRental.Users.API.Models
{
    public class Address : Entity
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string City { get; set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; protected set; }

        public string State { get; set; }

        protected Address() { }

        public Address(string zipCode, string street, string number, string complement, string city, string state)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            State = state;
        }
    }
}