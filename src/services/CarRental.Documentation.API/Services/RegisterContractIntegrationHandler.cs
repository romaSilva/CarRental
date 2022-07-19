using CarRental.Core.Messages.Integrations;
using CarRental.Documentation.API.Data;
using CarRental.Documentation.API.Models;
using CarRental.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRental.Documentation.API.Services
{
    public class RegisterContractIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterContractIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<RentalRegisteredIntegrationEvent>("RentalRegistered", async request =>
                await RegisterContact(request));
        }

        private async Task RegisterContact(RentalRegisteredIntegrationEvent message)
        {
            var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DocumentationContext>();

            context.Contracts.Add(MapToContract(message));

            await context.SaveChangesAsync();
        }

        private Contract MapToContract(RentalRegisteredIntegrationEvent message)
        {
            return new Contract(message.RentalId, message.SignerName, message.Cpf, message.PlateNumber, message.Model,
                                message.Year, message.RentDate, message.ReturnDate);
        }
    }
}
