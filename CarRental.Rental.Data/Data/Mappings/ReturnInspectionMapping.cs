using CarRental.Rental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Rental.Data.Data.Mappings
{
    public class ReturnInspectionMapping : IEntityTypeConfiguration<ReturnInspection>
    {
        public void Configure(EntityTypeBuilder<ReturnInspection> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.OperatorId)
                .IsRequired();

            builder.Property(c => c.Dirty)
                .IsRequired();

            builder.Property(c => c.EmptyTank)
                .IsRequired();

            builder.Property(c => c.Deformed)
                .IsRequired();

            builder.Property(c => c.Scratched)
                .IsRequired();

            builder.HasOne(c => c.Rental)
                .WithOne(c => c.ReturnInspection);

            builder.ToTable("ReturnInspections");
        }
    }
}
