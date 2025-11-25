using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Data.Configurations;

public class CampaingConfiguration : IEntityTypeConfiguration<Campaing>
{
    public void Configure(EntityTypeBuilder<Campaing> builder)
    {
        builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
    }
}
