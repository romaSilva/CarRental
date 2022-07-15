using CarRental.Users.API.Application;
using CarRental.Users.API.Data;
using CarRental.Users.API.Data.Repositories;
using CarRental.Users.API.Models;
using CarRental.Users.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Users.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IOperatorRepository, OperatorRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IUsersAppService, UsersAppService>();

            services.AddScoped<UsersContext>();
        }
    }
}
