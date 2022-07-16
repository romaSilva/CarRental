using CarRental.Users.API.Models;
using System;

namespace CarRental.Users.API.ViewModels
{
    public class CustomerViewModel
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; }
    }
}
