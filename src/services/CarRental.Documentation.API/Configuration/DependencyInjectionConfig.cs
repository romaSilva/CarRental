using CarRental.Documentation.API.Application;
using CarRental.Documentation.API.Data;
using CarRental.WebApi.Core.Identity;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Documentation.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();

            services.AddScoped<IPdfGenerator, PdfGenerator>();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddScoped<DocumentationContext>();
        }
    }
}
