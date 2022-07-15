using CarRental.Core.DomainObjects;

namespace CarRental.Fleet.API.Models
{
    public class Vehicle : Entity, IAggregateRoot
    {
        public string PlateNumber { get; private set; }
        public string Year { get; private set; }
        public double HourValue { get; private set; }
        public double BaggageSize { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public Category Category { get; private set; }
        public Fuel Fuel { get; private set; }

        protected Vehicle() { }

        public Vehicle(string plateNumber, string year, double hourValue, double baggageSize,
                       string brand, string model, Category category, Fuel fuel)
        {
            PlateNumber = plateNumber;
            Year = year;
            HourValue = hourValue;
            BaggageSize = baggageSize;
            Category = category;
            Fuel = fuel;
            Brand = brand;
            Model = model;
        }
    }
}
