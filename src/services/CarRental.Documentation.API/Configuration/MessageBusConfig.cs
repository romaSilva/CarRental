using CarRental.Core.Utils;
using CarRental.Documentation.API.Services;
using CarRental.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Documentation.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<RegisterContractIntegrationHandler>();
        }
    }
}
