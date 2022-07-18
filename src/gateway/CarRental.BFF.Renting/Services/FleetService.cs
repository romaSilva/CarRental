using CarRental.BFF.Renting.Extensions;
using CarRental.BFF.Renting.Models;
using CarRental.Core.DomainObjects;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public class FleetService : BaseService, IFleetService
    {
        private readonly HttpClient _httpClient;

        public FleetService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.FleetUrl);
        }

        public async Task<VehicleDto> GetVehicle(Guid vehicleId)
        {
            var response = await _httpClient.GetAsync($"vehicles/{vehicleId}");

            ManageResponseErrors(response);

            return await DeserializeResponse<VehicleDto>(response);
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByCategory(Category category)
        {
            var response = await _httpClient.GetAsync($"vehicles-in-category/{(int)category}");

            ManageResponseErrors(response);

            return await DeserializeResponse<IEnumerable<VehicleDto>>(response);
        }
    }
}
