using System;

namespace CarRental.Core.Messages.Integrations
{
    public class OperatorRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string CompanyRegistration { get; private set; }

        public OperatorRegisteredIntegrationEvent(Guid id, string name, string companyRegistration)
        {
            Id = id;
            Name = name;
            CompanyRegistration = companyRegistration;
        }
    }
}
