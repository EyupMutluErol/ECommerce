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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();//sql tarafında identity olmasını sağlar

            builder.Property(c=>c.UserId)
                .IsRequired()
                .HasMaxLength(50);


            //İlişki cart=>cartItems (1-N)
            builder.HasMany(c=>c.CartItems)
                .WithOne(ci=>ci.Cart)
                .HasForeignKey(ci=>ci.CartId)
                .OnDelete(DeleteBehavior.Cascade); //Bir sepet silindiğinde ilişkili sepet kalemleri de silinsin


            //İlişki cartItem=>whishlist

            builder.HasMany(c=>c.Wishlists)
                .WithOne()
                .HasForeignKey("CartId")
                .OnDelete(deleteBehavior:DeleteBehavior.Cascade); //Bir sepet silindiğinde ilişkili istek listesi kalemleri de silinsin


        }
    }
}
