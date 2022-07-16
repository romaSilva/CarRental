using CarRental.Rental.Data.Data;
using CarRental.WebApi.Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Rental.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();

            //// Commands
            //services.AddScoped<IRequestHandler<AdicionarPedidoCommand, ValidationResult>, PedidoCommandHandler>();

            //// Events
            //services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();

            //// Application
            //services.AddScoped<IMediatorHandler, MediatorHandler>();
            //services.AddScoped<IVoucherQueries, VoucherQueries>();
            //services.AddScoped<IPedidoQueries, PedidoQueries>();

            //// Data
            //services.AddScoped<IPedidoRepository, PedidoRepository>();
            //services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<RentalContext>();
        }
    }
}
