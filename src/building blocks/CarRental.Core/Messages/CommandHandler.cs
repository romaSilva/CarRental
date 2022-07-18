using CarRental.Core.Data;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddErrorMessage(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
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
