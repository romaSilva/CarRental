using CarRental.BFF.Renting.Extensions;
using CarRental.BFF.Renting.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public class RentalService : IRentalService
    {
        private readonly HttpClient _httpClient;

        public RentalService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.RentalUrl);
        }

        public Task<object> RequestRental(RentalDto rentalRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
