using System;

namespace CarRental.BFF.Renting.Models
{
    public class RentalDto
    {
        public Guid CustomerId { get; set; }
        public Guid VehicleId { get; set; }
        public string CustomerName { get; set; }
        public string Cpf { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public double HourValue { get; set; }
    }
}
