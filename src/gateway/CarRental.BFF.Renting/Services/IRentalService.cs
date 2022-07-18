using CarRental.BFF.Renting.Models;
using CarRental.BFF.Renting.ViewModels;
using CarRental.Core.Communication;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public interface IRentalService
    {
        Task<ResponseResult> RequestRental(RentalDto rentalRequest);
        Task<object> AddInspection(ReturnInspectionViewModel returnInspectionViewModel);
    }
}
