﻿using CarRental.Core.Messages;
using CarRental.Core.Messages.Integrations;
using CarRental.MessageBus;
using CarRental.Rental.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRental.Rental.API.Application.Commands
{
    public class RentalCommandHandler : CommandHandler,
                                        IRequestHandler<RequestRentalCommand, ValidationResult>,
                                        IRequestHandler<AddInspectionCommand, ValidationResult>
    {
        private readonly IVehicleRentalRepository _vehicleRentalRepository;
        private readonly IMessageBus _bus;


        public RentalCommandHandler(IVehicleRentalRepository vehicleRentalRepository, IMessageBus bus)
        {
            _vehicleRentalRepository = vehicleRentalRepository;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(RequestRentalCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var rentalRequest = CreateVehicleRental(request);

            _vehicleRentalRepository.Add(rentalRequest);

            var result = await SaveChanges(_vehicleRentalRepository.UnitOfWork);

            if (result.IsValid)
            {
                await _bus.PublishAsync(new RentalRegisteredIntegrationEvent(rentalRequest.Id, rentalRequest.CustomerName, rentalRequest.Cpf, rentalRequest.PlateNumber,
                                                                            rentalRequest.Model, rentalRequest.Year, rentalRequest.RentDate, rentalRequest.ReturnDate));
            }

            return result;
        }

        public async Task<ValidationResult> Handle(AddInspectionCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var rental = await _vehicleRentalRepository.GetById(request.RentalId);

            if (rental == null)
            {
                AddErrorMessage("Unable to find rental details");
                return ValidationResult;
            }

            var inspection = new ReturnInspection(request.OperatorId, request.Dirty, request.EmptyTank, request.Deformed, request.Scratched, rental.Id);

            if (rental.ReturnInspection != null)
            {
                _vehicleRentalRepository.RemoveInspection(rental.ReturnInspection);
            }

            rental.AddReturnInspection(inspection);
            _vehicleRentalRepository.Update(rental);
            _vehicleRentalRepository.AddInspection(inspection);

            return await SaveChanges(_vehicleRentalRepository.UnitOfWork);
        }

        private VehicleRental CreateVehicleRental(RequestRentalCommand request)
        {
            return new VehicleRental(request.CustomerId, request.VehicleId,
                                     request.CustomerName, request.Cpf, request.RentDate,
                                     request.ReturnDate, request.PlateNumber,
                                     request.Model, request.Year, request.HourValue);
        }
    }
}
