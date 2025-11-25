using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            //composite key 
            builder.HasKey(pc => new {pc.CategoryId,pc.ProductId});

            //İlişki: ProductCategory - Product (N - 1)

            builder.HasOne(pc=>pc.Category)
                .WithMany(c=>c.ProductCategories)
                .HasForeignKey(pc=>pc.CategoryId)
                .OnDelete(deleteBehavior:DeleteBehavior.Cascade); //Bir kategori silindiğinde ilişkili ürün kategorileri de silinsin


            //ilişki: ProductCategory - Product 

            builder.HasOne(pc=>pc.Product)
                .WithMany(p=>p.ProductCategories)
                .HasForeignKey(pc=>pc.ProductId)
                .OnDelete(deleteBehavior:DeleteBehavior.NoAction);

        }
    }
}
