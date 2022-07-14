using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.WebApi.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationIsValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                AddProcessingError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperationIsValid()
        {
            return !Errors.Any();
        }

        protected void AddProcessingError(string erro)
        {
            Errors.Add(erro);
        }

        protected void RemoveProcessingErrors()
        {
            Errors.Clear();
        }
    }
}
