using CarRental.Core.DomainObjects;
using System;

namespace CarRental.Rental.Domain.Models
{
    public class ReturnInspection : Entity
    {
        public Guid OperatorId { get; private set; }
        public bool Dirty { get; private set; }
        public bool EmptyTank { get; private set; }
        public bool Deformed { get; private set; }
        public bool Scratched { get; private set; }
        public Guid RentalId { get; private set; }
        public DateTime RegistryDate { get; private set; }

        public VehicleRental Rental { get; private set; }

        public ReturnInspection(Guid operatorId, bool dirty, bool emptyTank, bool deformed, bool scratched, Guid rentalId)
        {
            OperatorId = operatorId;
            Dirty = dirty;
            EmptyTank = emptyTank;
            Deformed = deformed;
            Scratched = scratched;
            RentalId = rentalId;
        }
    }
}