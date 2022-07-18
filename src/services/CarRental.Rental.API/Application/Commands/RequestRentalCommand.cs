using CarRental.Core.Messages;
using FluentValidation;
using System;

namespace CarRental.Rental.API.Application.Commands
{
    public class RequestRentalCommand : Command
    {
        public Guid CustomerId { get; set; }
        public Guid VehicleId { get; set; }
        public string CustomerName { get; set; }
        public string Cpf { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public double HourValue { get; set; }

        public bool IsValid()
        {
            ValidationResult = new RequestRentalValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RequestRentalValidation : AbstractValidator<RequestRentalCommand>
        {
            public RequestRentalValidation()
            {
                RuleFor(c => c.CustomerId)
                    .NotEmpty()
                    .WithMessage("CustomerId required");

                RuleFor(c => c.VehicleId)
                    .NotEmpty()
                    .WithMessage("VehicleId required");

                RuleFor(c => c.CustomerName)
                    .NotEmpty()
                    .WithMessage("CustomerName required");

                RuleFor(c => c.Cpf)
                    .Length(11)
                    .WithMessage("Invalid CPF");

                RuleFor(c => c.ReturnDate)
                    .GreaterThan(c => c.RentDate)
                    .WithMessage("Invalid rental dates");

                RuleFor(c => c.PlateNumber)
                    .NotEmpty()
                    .WithMessage("PlateNumber required");

                RuleFor(c => c.Model)
                    .NotEmpty()
                    .WithMessage("Model required");

                RuleFor(c => c.Year)
                    .NotEmpty()
                    .WithMessage("Year required");

                RuleFor(c => c.HourValue)
                    .GreaterThan(0)
                    .WithMessage("Invalid HourValue");
            }
        }
    }
}
