using InvoiceApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceApp.Configuration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(x => x.Vendor)
                .IsRequired();

            builder.Property(x => x.Vendee)
                .IsRequired();

            builder.Property(x => x.Products)
                .IsRequired();

            builder.Property(x => x.InvoiceNo)
                .IsRequired();

            builder.Property(x => x.PaymentMethod)
                .IsRequired();
        }
    }
}
