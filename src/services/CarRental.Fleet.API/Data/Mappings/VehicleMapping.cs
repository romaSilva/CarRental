using CarRental.Fleet.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Fleet.API.Data.Mappings
{
    public class VehicleMapping : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.PlateNumber)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Year)
                .IsRequired()
                .HasColumnType("varchar(4)");

            builder.Property(c => c.HourValue)
                .IsRequired();

            builder.Property(c => c.BaggageSize)
                .IsRequired();

            builder.Property(c => c.Brand)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Model)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Category)
               .IsRequired();

            builder.Property(c => c.Fuel)
               .IsRequired();

            builder.ToTable("Vehicles");
        }
    }
}
