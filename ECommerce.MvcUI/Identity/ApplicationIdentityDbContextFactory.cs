using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerce.MvcUI.Identity
{
    public class ApplicationIdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
    {
        public ApplicationIdentityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("IdentityConnection");
            var optionBuilder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            return new ApplicationIdentityDbContext(optionBuilder.Options);
        }
    }
}
