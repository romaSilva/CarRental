using System;

namespace CarRental.Core.Messages.Integrations
{
    public class CustomerRegisteredIntegationEvent : IntegationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }

        public CustomerRegisteredIntegationEvent(Guid id, string name, string cpf, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            BirthDate = birthDate;
        }
    }
}
