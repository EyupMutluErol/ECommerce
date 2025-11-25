using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Configurations
{
    public class CustomerCardConfiguration : IEntityTypeConfiguration<CustomerCard>

    {
        public void Configure(EntityTypeBuilder<CustomerCard> builder)
        {

           builder.Property(c=>c.UserId).IsRequired().HasMaxLength(50);
           builder.Property(c=>c.CardNumber).IsRequired().HasMaxLength(16);
            builder.Property(c => c.CardTitle).IsRequired().HasMaxLength(100);
            builder.Property(c=>c.ExpirationMonth).IsRequired().HasMaxLength(2);
            builder.Property(c=>c.ExpirationYear).IsRequired().HasMaxLength(4);
            builder.Property(c=>c.Cvv).IsRequired().HasMaxLength(3);


        }
    }
}
