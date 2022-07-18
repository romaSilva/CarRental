using CarRental.BFF.Renting.Models;
using CarRental.BFF.Renting.ViewModels;
using CarRental.Core.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public interface IRentalsService
    {
        Task<ResponseResult> RequestRental(RentalDto rentalRequest);
        Task<ResponseResult> AddInspection(ReturnInspectionViewModel returnInspectionViewModel);
        Task<IEnumerable<RentalDto>> GetRentalsInProgress();
    }
}
