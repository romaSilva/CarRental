using CarRental.Core.Messages;
using FluentValidation;
using System;

namespace CarRental.Rental.API.Application.Commands
{
    public class AddInspectionCommand : Command
    {
        public Guid OperatorId { get; set; }
        public bool Dirty { get; set; }
        public bool EmptyTank { get; set; }
        public bool Deformed { get; set; }
        public bool Scratched { get; set; }
        public Guid RentalId { get; set; }

        public bool IsValid()
        {
            ValidationResult = new AddInspectionValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AddInspectionValidation : AbstractValidator<AddInspectionCommand>
        {
            public AddInspectionValidation()
            {
                RuleFor(c => c.OperatorId)
                    .NotEmpty()
                    .WithMessage("OperatorId required");

                RuleFor(c => c.RentalId)
                    .NotEmpty()
                    .WithMessage("RentalId required");
            }
        }
    }
}
