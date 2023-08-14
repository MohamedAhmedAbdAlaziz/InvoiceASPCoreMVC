
using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastrucrture.Infrastrucrture.Config
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.HasIndex(u => u.Code)
          .IsUnique();
            // builder.Property(p => p.Date).IsRequired();
            builder.Property(x => x.Date)
          .HasConversion<DateOnlyConverter, DateOnlyComparer>();
            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder
          .HasMany(e => e.Items)
           .WithOne(e => e.Invoice)
          .HasForeignKey(e => e.InvoiceId)
          .IsRequired();
          builder.HasOne(b => b.Customer).WithMany()
         .HasForeignKey(p => p.CustomerId);
        }
    }
}
