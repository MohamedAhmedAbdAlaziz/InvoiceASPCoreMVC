
using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucrture.Infrastrucrture.Config
{
    public class CustomerConfiguration :IEntityTypeConfiguration<Customer>
    {
     
         public void Configure(EntityTypeBuilder<Customer> builder)
    {

            builder.Property(p => p.Id).IsRequired();
           builder.Property(p => p.Name).IsRequired();

        }
    }
}
