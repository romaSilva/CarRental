using CarRental.Core.Data;
using CarRental.Rental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Rental.Data.Data
{
    public class RentalContext : DbContext, IUnitOfWork
    {
        //private readonly IMediatorHandler _mediatorHandler;

        public RentalContext(DbContextOptions<RentalContext> options)
            : base(options)
        {
            //_mediatorHandler = mediatorHandler;
        }

        public DbSet<VehicleRental> Rentals { get; set; }
        public DbSet<ReturnInspection> Inspections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("RegistryDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegistryDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegistryDate").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }

    //public static class MediatorExtension
    //{
    //    public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    //    {
    //        var domainEntities = ctx.ChangeTracker
    //            .Entries<Entity>()
    //            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

    //        var domainEvents = domainEntities
    //            .SelectMany(x => x.Entity.Notificacoes)
    //            .ToList();

    //        domainEntities.ToList()
    //            .ForEach(entity => entity.Entity.LimparEventos());

    //        var tasks = domainEvents
    //            .Select(async (domainEvent) =>
    //            {
    //                await mediator.PublicarEvento(domainEvent);
    //            });

    //        await Task.WhenAll(tasks);
    //    }
    //}
}
