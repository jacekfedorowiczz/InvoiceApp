using InvoiceApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceApp.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.HashedPassword) 
                .IsRequired();

            builder.HasMany(x => x.Invoices)
                .WithOne(i => i.CreatedBy)
                .HasForeignKey(i => i.CreatedById);
        }
    }
}
