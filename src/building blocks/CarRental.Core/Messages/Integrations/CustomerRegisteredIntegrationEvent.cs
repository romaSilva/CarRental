using System;

namespace CarRental.Core.Messages.Integrations
{
    public class CustomerRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }

        public CustomerRegisteredIntegrationEvent(Guid id, string name, string cpf, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            BirthDate = birthDate;
        }
    }
}
