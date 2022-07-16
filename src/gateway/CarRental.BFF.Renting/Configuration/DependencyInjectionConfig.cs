using CarRental.BFF.Renting.Extensions;
using CarRental.BFF.Renting.Services;
using CarRental.WebApi.Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.BFF.Renting.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IFleetService, FleetService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IRentalService, RentalService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IUsersService, UsersService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        }
    }
}
