using CarRental.Core.Messages;
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
    }
}
