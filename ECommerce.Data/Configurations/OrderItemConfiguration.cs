using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ECommerce.Data.Configurations
{
    //LLM,MCP,RAG 
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");

            builder.Property(e => e.DiscountPrice)
                .IsRequired()
                .HasPrecision(18, 1);

            builder.Property(e => e.Quantity)
                .IsRequired();


            //İlişki: OrderItem - Product (N - 1)

            builder.HasOne(oi=>oi.Order)
                .WithMany(o=>o.OrderItems)
                .HasForeignKey(oi=>oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); //Bir sipariş silindiğinde ilişkili sipariş kalemleri de silinsin



            builder.HasOne(oi=>oi.Product)
                .WithMany()
                .HasForeignKey(oi=>oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict); //Bir ürün silindiğinde ilişkili sipariş kalemleri silinmesin, hata versin



        }

    }
}
