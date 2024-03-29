﻿using CarRental.Core.Messages.Integrations;
using CarRental.MessageBus;
using CarRental.Users.API.Application;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRental.Users.API.Services
{
    public class RegisterOperatorIntegrationHandler : BackgroundService
    {
        private IMessageBus _bus;
        private IServiceProvider _serviceProvider;

        public RegisterOperatorIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<OperatorRegisteredIntegrationEvent, ResponseMessage>(async request =>
                await CreateOperator(request));
        }

        private async Task<ResponseMessage> CreateOperator(OperatorRegisteredIntegrationEvent message)
        {
            ValidationResult result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IUsersAppService>();
                result = await service.CreateOperator(message);
            }

            return new ResponseMessage(result);
        }
    }
}
