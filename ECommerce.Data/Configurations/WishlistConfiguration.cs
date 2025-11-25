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
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(w => w.Id);//primary key

            //whislist -product ilişkisi (N-1)
            builder.HasOne(w=>w.Product)
                .WithMany()
                .HasForeignKey(w=>w.ProductId)
                .OnDelete(DeleteBehavior.Restrict); //Bir ürün silindiğinde ilişkili whislist kalemleri silinmesin, hata versin


            builder.HasOne(w=>w.Cart)
                .WithMany(c=>c.Wishlists)
                .HasForeignKey(w=>w.CartId)
                .OnDelete(deleteBehavior:DeleteBehavior.Cascade); //Bir sepet silindiğinde ilişkili whislist kalemleri de silinsin



        }
    }
}
