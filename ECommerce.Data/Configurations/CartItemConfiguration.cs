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
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(ci=>ci.Quantity)
                .IsRequired();

            //ilişki : CartItem - Product (N - 1)
            builder.HasOne(ci=>ci.Product)
                .WithMany()
                .HasForeignKey(ci=>ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict); //Bir ürün silindiğinde ilişkili sepet kalemleri silinmesin, hata versin


            //ilişki :CartItem- Cart

            builder.HasOne(ci=>ci.Cart)
                .WithMany(c=>c.CartItems)
                .HasForeignKey(ci=>ci.CartId)
                .OnDelete(DeleteBehavior.Cascade); //Bir sepet silindiğinde ilişkili sepet kalemleri de silinsin



        }
    }
}
