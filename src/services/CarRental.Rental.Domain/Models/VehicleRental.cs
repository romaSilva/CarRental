using CarRental.Core.DomainObjects;
using System;

namespace CarRental.Rental.Domain.Models
{
    public class VehicleRental : Entity, IAggregateRoot
    {
        public Guid CustomerId { get; private set; }
        public Guid VehicleId { get; private set; }
        public string CustomerName { get; private set; }
        public string Cpf { get; private set; }
        public DateTime RentDate { get; private set; }
        public DateTime ReturnDate { get; private set; }
        public string PlateNumber { get; private set; }
        public string Model { get; private set; }
        public string Year { get; private set; }
        public double HourValue { get; private set; }
        public double InitialTotalValue { get; private set; }
        public double AdditionalValue { get; private set; }
        public RentalStatus Status { get; private set; }
        public DateTime RegistryDate { get; private set; }

        public ReturnInspection ReturnInspection { get; private set; }

        protected VehicleRental() { }

        public VehicleRental(Guid customerId, Guid vehicleId, string customerName, string cpf, DateTime rentDate,
                      DateTime returnDate, string plateNumber, string model, string year, double hourValue)
        {
            CustomerId = customerId;
            VehicleId = vehicleId;
            CustomerName = customerName;
            Cpf = cpf;
            RentDate = rentDate;
            ReturnDate = returnDate;
            PlateNumber = plateNumber;
            Model = model;
            Year = year;
            HourValue = hourValue;

            InitialTotalValue = Math.Round(hourValue * (ReturnDate - RentDate).TotalHours, 2);
            AdditionalValue = 0;
            Status = RentalStatus.Reserved;
        }

        public void ExtendRental(DateTime newReturnDate)
        {
            var additionalHours = (newReturnDate - ReturnDate).TotalHours;
            ReturnDate = newReturnDate;
            AdditionalValue = additionalHours * HourValue;
        }

        public void AddReturnInspection(ReturnInspection returnInspection)
        {
            ReturnInspection = returnInspection;
            Status = RentalStatus.Returned;
            AddInspectionCosts();
        }

        private void AddInspectionCosts()
        {
            var insppectionAddOn = Math.Round(InitialTotalValue * 0.3);

            AdditionalValue += ReturnInspection.Dirty ? insppectionAddOn : 0;
            AdditionalValue += ReturnInspection.EmptyTank ? insppectionAddOn : 0;
            AdditionalValue += ReturnInspection.Deformed ? insppectionAddOn : 0;
            AdditionalValue += ReturnInspection.Scratched ? insppectionAddOn : 0;
        }
    }
}
