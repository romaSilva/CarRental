using CarRental.Users.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Users.API.Data.Mappings
{
    public class OperatorMapping : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.CompanyRegistration)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Operators");
        }
    }
}
