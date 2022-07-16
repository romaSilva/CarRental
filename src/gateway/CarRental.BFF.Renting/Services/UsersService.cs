using CarRental.BFF.Renting.Extensions;
using CarRental.BFF.Renting.Models;
using CarRental.Core.DomainObjects;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly HttpClient _httpClient;

        public UsersService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.UsersUrl);
        }

        public async Task<CustomerDto> GetCustomer(Guid userId)
        {
            var response = await _httpClient.GetAsync($"customers/{userId}");

            ManageResponseErrors(response);

            return await DeserializeResponse<CustomerDto>(response);
        }
    }
}
