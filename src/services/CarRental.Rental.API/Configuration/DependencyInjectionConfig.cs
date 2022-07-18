using CarRental.Core.Mediator;
using CarRental.Rental.API.Application.Commands;
using CarRental.Rental.API.Application.Queries;
using CarRental.Rental.Data.Data;
using CarRental.Rental.Data.Data.Repositories;
using CarRental.Rental.Domain.Models;
using CarRental.WebApi.Core.Identity;
using FluentValidation.Results;
using MediatR;
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

            services.AddScoped<IRequestHandler<RequestRentalCommand, ValidationResult>, RentalCommandHandler>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRentalQueries, RentalQueries>();

            services.AddScoped<IVehicleRentalRepository, VehicleRentalRepository>();
            services.AddScoped<RentalContext>();
        }
    }
}
