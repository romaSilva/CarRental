using CarRental.Users.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Users.API.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfLength})");
            });

            builder.Property(c => c.BirthDate)
                .IsRequired();

            builder.HasOne(c => c.Address)
                .WithOne(a => a.Customer);

            builder.ToTable("Customers");
        }
    }
}
