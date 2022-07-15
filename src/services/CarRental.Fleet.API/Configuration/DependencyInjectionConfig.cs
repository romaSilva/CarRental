using CarRental.Fleet.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Fleet.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<FleetContext>();
        }
    }
}
