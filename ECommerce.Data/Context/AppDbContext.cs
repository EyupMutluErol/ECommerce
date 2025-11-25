using ECommerce.Data.Configurations;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Context;

public class AppDbContext : DbContext
{

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }



    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Campaing> Campaings { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<CustomerCard> CustomerCards { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }

}