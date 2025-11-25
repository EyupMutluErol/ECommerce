using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Data.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(500);
            builder.Property(e => e.City).IsRequired().HasMaxLength(50);
            builder.Property(e => e.District).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Neighborhood).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Phone).IsRequired().HasMaxLength(15);
            builder.Property(e => e.AddressTitle).HasMaxLength(100);
        }
    }
}
