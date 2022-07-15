using CarRental.Core.DomainObjects;
using System;
using System.Text.Json.Serialization;

namespace CarRental.Users.API.Models
{
    public class Address : Entity
    {
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid CustomerId { get; private set; }

        [JsonIgnore]
        public Customer Customer { get; protected set; }

        protected Address() { }

        public Address(string zipCode, string street, string number, string complement, string city, string state, Guid customerId)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            State = state;
            CustomerId = customerId;
        }
    }
}