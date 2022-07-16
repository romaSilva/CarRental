using CarRental.BFF.Renting.Models;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public interface IRentalService
    {
        Task<object> RequestRental(RentalDto rentalRequest);
    }
}
