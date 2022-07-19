using CarRental.Core.DomainObjects;
using System;

namespace CarRental.Documentation.API.Models
{
    public class Contract : Entity, IAggregateRoot
    {
        public Guid RentalId { get; private set; }
        public string SignerName { get; private set; }
        public string Cpf { get; private set; }
        public string PlateNumber { get; private set; }
        public string Model { get; private set; }
        public string Year { get; private set; }
        public DateTime RentDate { get; private set; }
        public DateTime ReturnDate { get; private set; }
        public bool Signed { get; private set; }

        protected Contract() { }

        public Contract(Guid rentalId, string signerName, string cpf, string plateNumber, string model, string year, DateTime rentDate, DateTime returnDate)
        {
            RentalId = rentalId;
            SignerName = signerName;
            Cpf = cpf;
            PlateNumber = plateNumber;
            Model = model;
            Year = year;
            RentDate = rentDate;
            ReturnDate = returnDate;
            Signed = false;
        }

        public void Sign()
        {
            Signed = true;
        }
    }
}
