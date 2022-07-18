using CarRental.Rental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Rental.Data.Data.Mappings
{
    public class VehicleRentalMapping : IEntityTypeConfiguration<VehicleRental>
    {
        public void Configure(EntityTypeBuilder<VehicleRental> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CustomerId)
                .IsRequired();

            builder.Property(c => c.VehicleId)
                .IsRequired();

            builder.Property(c => c.CustomerName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(c => c.RentDate)
                .IsRequired();

            builder.Property(c => c.ReturnDate)
                .IsRequired();

            builder.Property(c => c.PlateNumber)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(c => c.Model)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.HourValue)
                .IsRequired();

            builder.Property(c => c.InitialTotalValue)
                .IsRequired();

            builder.Property(c => c.AdditionalValue)
                .IsRequired();

            builder.HasOne(c => c.ReturnInspection)
                .WithOne(c => c.Rental);

            builder.Property(c => c.RegistryDate)
                .IsRequired();

            builder.ToTable("VehicleRentals");
        }
    }
}
