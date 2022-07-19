using CarRental.Core.DomainObjects;
using CarRental.Documentation.API.Application;
using CarRental.Documentation.API.Data;
using CarRental.WebApi.Core.Controllers;
using CarRental.WebApi.Core.Identity;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Documentation.API.Controllers
{
    public class ContractsController : MainController
    {
        private readonly DocumentationContext _context;
        private readonly IPdfGenerator _pdfGenerator;

        public ContractsController(DocumentationContext context, IPdfGenerator pdfGenerator)
        {
            _context = context;
            _pdfGenerator = pdfGenerator;
        }

        [HttpGet("{rentalId}")]
        [ClaimsAuthorize(Constants.Claims.Role, Constants.Roles.Customer)]
        public async Task<IActionResult> GeneratePDF(Guid rentalId)
        {
            var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.RentalId == rentalId);

            if (contract == null)
            {
                AddProcessingError("Contract not generated, try again later");
                return CustomResponse();
            }

            return File(_pdfGenerator.GenerateContractPdf(contract),
            "application/octet-stream", "DemoPdf.pdf");
        }
    }
}
