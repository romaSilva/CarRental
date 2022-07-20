using CarRental.Core.Messages;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace CarRental.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T comando) where T : Command;
    }
}
