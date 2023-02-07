using InvoiceApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceApp.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(X => X.Name)
                .IsRequired();

            builder.Property(X => X.Country)
                .IsRequired();

            builder.Property(X => X.PostalCode)
                .IsRequired();

            builder.Property(X => X.City)
                .IsRequired();

            builder.Property(X => X.Street)
                .IsRequired();

            builder.HasMany(x => x.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId);
        }
    }
}
