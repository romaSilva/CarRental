using CarRental.BFF.Renting.Extensions;
using CarRental.BFF.Renting.Models;
using CarRental.BFF.Renting.ViewModels;
using CarRental.Core.Communication;
using CarRental.Core.DomainObjects;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRental.BFF.Renting.Services
{
    public class RentalService : BaseService, IRentalsService
    {
        private readonly HttpClient _httpClient;

        public RentalService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.RentalsUrl);
        }

        public async Task<ResponseResult> AddInspection(ReturnInspectionDto returnInspectionViewModel)
        {
            var requestBody = Serialize(returnInspectionViewModel);

            var response = await _httpClient.PostAsync($"add-inspection", requestBody);

            if (!ManageResponseErrors(response)) return await DeserializeResponse<ResponseResult>(response);

            return ReturnSuccessfulResult();
        }

        public async Task<ResponseResult> RequestRental(RentalDto rentalRequest)
        {
            var requestBody = Serialize(rentalRequest);

            var response = await _httpClient.PostAsync($"rent-vehicle", requestBody);

            if (!ManageResponseErrors(response)) return await DeserializeResponse<ResponseResult>(response);

            return ReturnSuccessfulResult();
        }

        public async Task<IEnumerable<RentalDto>> GetRentalsInProgress()
        {
            var response = await _httpClient.GetAsync($"in-progress");

            ManageResponseErrors(response);

            return await DeserializeResponse<IEnumerable<RentalDto>>(response);
        }
    }
}
