using System;
using System.Text.Json.Serialization;

namespace CarRental.BFF.Renting.ViewModels
{
    public class ReturnInspectionViewModel
    {
        public bool Dirty { get; set; }
        public bool EmptyTank { get; set; }
        public bool Deformed { get; set; }
        public bool Scratched { get; set; }
        public Guid RentalId { get; set; }
    }

    public class ReturnInspectionDto : ReturnInspectionViewModel
    {
        public Guid OperatorId { get; set; }

        public ReturnInspectionDto(ReturnInspectionViewModel returnInspectionViewModel)
        {
            Dirty = returnInspectionViewModel.Dirty;
            EmptyTank = returnInspectionViewModel.EmptyTank;
            Deformed = returnInspectionViewModel.Deformed;
            Scratched = returnInspectionViewModel.Scratched;
            RentalId = returnInspectionViewModel.RentalId;
        }
    }

}
