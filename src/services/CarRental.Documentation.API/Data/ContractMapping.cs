using CarRental.Documentation.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Documentation.API.Data
{
    public class ContractMapping : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(c => c.PlateNumber)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Model)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Year)
                .IsRequired()
                .HasColumnType("varchar(4)");

            builder.Property(c => c.RentDate)
                .IsRequired();

            builder.Property(c => c.ReturnDate)
                .IsRequired();

            builder.Property(c => c.Signed)
                .IsRequired();

            builder.ToTable("Contracts");
        }
    }
}
