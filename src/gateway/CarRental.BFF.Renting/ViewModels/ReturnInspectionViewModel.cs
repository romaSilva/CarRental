using System;
using System.Text.Json.Serialization;

namespace CarRental.BFF.Renting.ViewModels
{
    public class ReturnInspectionViewModel
    {
        [JsonIgnore]
        public Guid OperatorId { get; set; } = Guid.Empty;
        public bool Dirty { get; set; }
        public bool EmptyTank { get; set; }
        public bool Deformed { get; set; }
        public bool Scratched { get; set; }
        public Guid RentalId { get; set; }
    }
}
