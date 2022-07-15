using CarRental.Fleet.API.Models;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Fleet.API.ViewModels
{
    public class VehicleViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Year { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public double HourValue { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public double BaggageSize { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public Fuel Fuel { get; set; }
    }
}
