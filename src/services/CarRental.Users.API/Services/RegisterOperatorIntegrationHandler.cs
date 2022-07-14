using CarRental.Core.Messages.Integrations;
using CarRental.MessageBus;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace CarRental.Users.API.Services
{
    public class RegisterOperatorIntegrationHandler : BackgroundService
    {
        private IMessageBus _bus;
        private IServiceCollection _serviceCollection;

        public RegisterOperatorIntegrationHandler(IMessageBus bus, IServiceCollection serviceCollection)
        {
            _bus = bus;
            _serviceCollection = serviceCollection;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RespondAsync<OperatorRegisteredIntegrationEvent, ResponseMessage>(async request =>
            new ResponseMessage(new ValidationResult())); //Chamar cadastro de user

            return Task.CompletedTask;
        }
    }
}
