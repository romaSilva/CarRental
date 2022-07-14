using System;

namespace CarRental.Core.Messages.Integrations
{
    public class OperatorRegisteredIntegationEvent : IntegationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string CompanyRegistration { get; private set; }

        public OperatorRegisteredIntegationEvent(Guid id, string name, string companyRegistration)
        {
            Id = id;
            Name = name;
            CompanyRegistration = companyRegistration;
        }
    }
}
