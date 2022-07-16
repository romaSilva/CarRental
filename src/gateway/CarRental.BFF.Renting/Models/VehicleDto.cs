using System;

namespace CarRental.BFF.Renting.Models
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string PlateNumber { get; set; }
        public string Year { get; set; }
        public double HourValue { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
    }
}
