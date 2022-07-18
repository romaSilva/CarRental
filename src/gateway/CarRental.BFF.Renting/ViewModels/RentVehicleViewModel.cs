using CarRental.BFF.Renting.Models;
using System;

namespace CarRental.BFF.Renting.ViewModels
{
    public class RentVehicleViewModel
    {
        public Category Category { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
