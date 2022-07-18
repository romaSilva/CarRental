using System;

namespace CarRental.Rental.API.Application.Queries
{
    public class VehicleRentalDto
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
        public double InitialTotalValue { get; set; }
        public double AdditionalValue { get; set; }
        public double TotalValue { get; set; }
    }
}
