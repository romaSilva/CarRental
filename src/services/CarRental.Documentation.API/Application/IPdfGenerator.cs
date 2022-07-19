using CarRental.Documentation.API.Models;

namespace CarRental.Documentation.API.Application
{
    public interface IPdfGenerator
    {
        byte[] GenerateContractPdf(Contract contract);
    }
}
