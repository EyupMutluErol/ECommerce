using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ECommerce.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>

{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Brand).IsRequired().HasMaxLength(100);
        builder.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
        builder.Property(e => e.DiscountedPrice).HasColumnType("decimal(18,2)");
        builder.Property(e => e.StockQuantity).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();


        //Sayfa gösterim flaglarını varsayılan olarak false 

        builder.Property(e => e.ShowOnPageAsDailyHighlight).HasDefaultValue(false);
        builder.Property(e => e.ShowOnPageAsMonthlyHighlight).HasDefaultValue(false);
        builder.Property(e => e.ShowOnPageAsPopular).HasDefaultValue(false);
        builder.Property(e => e.ShowOnPageAsSpecialOffer).HasDefaultValue(false);


       
    
    }
}

