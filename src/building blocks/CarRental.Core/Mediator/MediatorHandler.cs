using CarRental.Core.Messages;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T message) where T : Event
        {
            await _mediator.Publish(message);
        }

        public async Task<ValidationResult> SendCommand<T>(T message) where T : Command
        {
            return await _mediator.Send(message);
        }
    }
}
