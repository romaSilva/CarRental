using CarRental.Core.Data;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
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
