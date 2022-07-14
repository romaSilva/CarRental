using System.Threading.Tasks;

namespace CarRental.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}