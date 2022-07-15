using CarRental.Core.Data;
using CarRental.Fleet.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Fleet.API.Data
{
    public class FleetContext : DbContext, IUnitOfWork
    {
        public FleetContext(DbContextOptions<FleetContext> options)
           : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
