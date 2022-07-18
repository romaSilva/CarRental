using CarRental.Rental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Rental.API.Application.Queries
{
    public class RentalQueries : IRentalQueries
    {
        private readonly IVehicleRentalRepository _vehicleRentalRepository;

        public async Task<IEnumerable<VehicleRentalDto>> GetRentals()
        {
            return MapToVehicleRentalDto(await _vehicleRentalRepository.GetAll());
        }

        public async Task<IEnumerable<VehicleRentalDto>> GetRentalsByCustomer(Guid customerId)
        {
            return MapToVehicleRentalDto(await _vehicleRentalRepository.GetByCustomer(customerId));
        }

        private IEnumerable<VehicleRentalDto> MapToVehicleRentalDto(IEnumerable<VehicleRental> rentals)
        {
            var rentalDtos = new List<VehicleRentalDto>();

            foreach (var rental in rentals)
            {
                rentalDtos.Add(new VehicleRentalDto
                {
                    CustomerId = rental.CustomerId,
                    VehicleId = rental.VehicleId,
                    CustomerName = rental.CustomerName,
                    Cpf = rental.Cpf,
                    RentDate = rental.RentDate,
                    ReturnDate = rental.ReturnDate,
                    PlateNumber = rental.PlateNumber,
                    Model = rental.Model,
                    Year = rental.Year,
                    HourValue = rental.HourValue,
                    InitialTotalValue = rental.InitialTotalValue,
                    AdditionalValue = rental.AdditionalValue,
                    TotalValue = rental.InitialTotalValue + rental.AdditionalValue

                });
            }

            return rentalDtos;
        }
    }
}
