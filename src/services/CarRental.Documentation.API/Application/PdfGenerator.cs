using CarRental.Documentation.API.Models;
using DinkToPdf;
using DinkToPdf.Contracts;
using System;

namespace CarRental.Documentation.API.Application
{
    public class PdfGenerator : IPdfGenerator
    {
        private readonly IConverter _converter;

        public PdfGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GenerateContractPdf(Contract contract)
        {
            var html = GenerateTemplate(contract);
            return _converter.Convert(CreateConfigurations(html));
        }

        private IDocument CreateConfigurations(string html)
        {
            GlobalSettings globalSettings = new GlobalSettings
            {
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,

                Margins = new MarginSettings { Top = 25, Bottom = 25 }
            };

            ObjectSettings objectSettings = new ObjectSettings
            {
                HtmlContent = html
            };

            return new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
        }

        private string GenerateTemplate(Contract contract)
        {
            return $@"
            <!DOCTYPE html>
            <html lang=""en"">
                <head>
                </head>
                <body>
                    <h1>Rental Contract</h1>
                    <p>{contract.SignerName}</p>
                    <p>{contract.Cpf}</p>
                    <p>{contract.PlateNumber}</p>
                    <p>{contract.Model}</p>
                    <p>{contract.Year}</p>
                    <p>{contract.RentDate}</p>
                    <p>{contract.ReturnDate}</p>
                </body>
            </html>
          ";
        }
    }
}
