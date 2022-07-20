using CarRental.Rental.API.Application.Commands;
using System;
using System.Threading;
using Xunit;
using Moq.AutoMock;
using CarRental.Rental.Domain.Models;
using CarRental.MessageBus;
using Moq;
using CarRental.Core.Messages.Integrations;
using System.Threading.Tasks;
using System.Linq;

namespace CarRental.Rentals.Test
{
    public class RentalRequestTests
    {
        private readonly CancellationToken cancellationToken;

        private readonly RentalCommandHandler _handler;
        private readonly Mock<IVehicleRentalRepository> _repository;
        private readonly Mock<IMessageBus> _bus;

        public RentalRequestTests()
        {
            var mocker = new AutoMocker();

            _repository = mocker.GetMock<IVehicleRentalRepository>();
            _bus = mocker.GetMock<IMessageBus>();

            _handler = new RentalCommandHandler(_repository.Object, _bus.Object);
        }

        [Fact]
        public async void HandleRentalRequest_InvalidCommand_ShoudReturnValidationError()
        {
            var command = new RequestRentalCommand();

            var result = await _handler.Handle(command, cancellationToken);

            Assert.True(!result.IsValid);
        }

        [Fact]
        public async void HandleRentalRequest_InvalidCommand_ShoudNotPublishEvent()
        {
            var command = new RequestRentalCommand();

            var result = await _handler.Handle(command, cancellationToken);

            _bus.Verify(r => r.PublishAsync(It.IsAny<IntegrationEvent>()), Times.Never);
        }

        [Fact]
        public async void HandleAddInspection_InvalidCommand_ShoudReturnValidationError()
        {
            var command = new AddInspectionCommand();

            var result = await _handler.Handle(command, cancellationToken);

            Assert.True(!result.IsValid);
        }

        [Fact]
        public async void HandleAddInspection_RentalNotFound_ShoudReturnValidationError()
        {
            _repository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(Task.FromResult<VehicleRental>(null));

            var command = new AddInspectionCommand()
            {
                OperatorId = Guid.NewGuid(),
                RentalId = Guid.NewGuid(),
                Dirty = true,
                EmptyTank = true,
                Deformed = false,
                Scratched = false
            };

            var result = await _handler.Handle(command, cancellationToken);

            Assert.True(!result.IsValid);
            Assert.True(result.Errors.FirstOrDefault().ErrorMessage.Equals("Unable to find rental details"));
        }
    }
}
