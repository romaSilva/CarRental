using System;

namespace CarRental.BFF.Renting.ViewModels
{
    public class RentVehicleViewModel
    {
        public Guid VehicleId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
