using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.BFF.Renting.ViewModels
{
    public class SimulateRentalPriceViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public Guid VehicleId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime RentDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime ReturnDate { get; set; }
    }
}
