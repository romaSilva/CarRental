using CarRental.Core.Data;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarRental.Core.DomainObjects
{
    public abstract class BaseService
    {
        protected ValidationResult ValidationResult;

        protected BaseService()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddErrorMessage(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool ManageResponseErrors(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected async Task<ValidationResult> SaveChanges(IUnitOfWork uow)
        {
            if (!await uow.Commit())
            {
                AddErrorMessage("Something went wrong persisting the data");
            }

            return ValidationResult;
        }
    }
}
