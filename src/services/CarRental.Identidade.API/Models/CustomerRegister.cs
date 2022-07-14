﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Identidade.API.Models
{
    public class CustomerRegister
    {
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(11, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} Invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; }
    }
}

