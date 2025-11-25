using ECommerce.Business;
using ECommerce.Data.Context;
using ECommerce.MvcUI.EmailServices;
using ECommerce.MvcUI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.MvcUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddBusinessServices(builder.Configuration);

            builder.Services.AddDbContext<ApplicationIdentityDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {

                //password settings
                options.Password.RequireDigit = true; // en az bir rakam zorunlu
                options.Password.RequireLowercase = true; // en az bir kucuk harf zorunlu
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false; // özel karakter zorunlu degil
                options.Password.RequireUppercase = false; // en az bir büyük harf zorunlu


                //Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //User settings
                options.User.RequireUniqueEmail = true;

                //SignIn settings
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = true;

                //Auth2 ,Keyclock=> database postgresql => javadýr. 
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = "/Admin/Login";
                options.LogoutPath = "/Admin/Logout";
                options.AccessDeniedPath = "/Admin/AccessDenied";
                options.SlidingExpiration = false;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);

                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = "ECommerce.MvcUI.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };

            });
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(x =>
                new SmtpEmailSender(
                    builder.Configuration["EmailSender:Host"] ?? "",
                    builder.Configuration.GetValue<int>("EmailSender:Port"),
                    builder.Configuration["EmailSender:UserName"] ?? "",
                    builder.Configuration["EmailSender:Password"] ?? "",
                    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL")
                                  
            ));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();



            app.MapControllerRoute(

                name: "admin",
                pattern: "admin",
                defaults: new { controller = "AdminLogin", action = "AdminLogin" }

              );

            app.MapControllerRoute(

                name: "adminProducts",
                pattern: "admin/products",
                defaults: new { controller = "Admin", action = "ProductList" }
                );

            app.MapControllerRoute(

                name: "adminProductEdit",
                pattern: "admin/products/edit/{id?}", //id parametresi opsiyonel hangi id güncellicez? 
                defaults: new { controller = "Admin", action = "EditProduct" }

                );

            app.MapControllerRoute(

                name: "cart",
                pattern: "cart",
                defaults: new { controller = "Cart", action = "Index" }
                );
            app.MapControllerRoute(
                name: "orders",
                pattern: "orders",
                defaults: new { controller = "Order", action = "GetOrders" }

                );
            app.MapControllerRoute(

                name: "checkout",
                pattern: "checkout",
                defaults: new { controller = "Cart", action = "Checkout" }
                );

            app.MapControllerRoute(
                name: "products",
                pattern: "products/{category?}",
                defaults: new { controller = "HomePage", action = "ProductList" }
                );


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=HomePage}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.Run();
        }
    }
}
