using CarRental.Core.DomainObjects;
using System;

namespace CarRental.Users.API.Models
{
    public class Operator : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string CompanyRegistration { get; private set; }

        protected Operator() { }

        public Operator(Guid id, string name, string companyRegistration)
        {
            Id = id;
            Name = name;
            CompanyRegistration = companyRegistration;
        }
    }
}
