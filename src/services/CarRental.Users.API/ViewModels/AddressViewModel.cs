using System.ComponentModel.DataAnnotations;

namespace CarRental.Users.API.ViewModels
{
    public class AddressViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string State { get; set; }
    }
}
