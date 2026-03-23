using InventoryOrderingSystem.Models.Database;
using InventoryOrderingSystem.Repository.Admins;
using InventoryOrderingSystem.Repository.Customers;
using InventoryOrderingSystem.Service.Admins;
using InventoryOrderingSystem.Service.Customers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace InventoryOrderingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Construct Database Context
            builder.Services.AddDbContext<InventoryOrderingSystemContext>(options =>
            {
                //Get Connection String
                options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryOrderingSystem"));
            });

            //Let us add the services and repositories
            builder.Services.AddScoped<IAdminsService, AdminsService>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            //Future Proofing adding the authentication 
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // Redirect to login page if not authenticated
                    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to access denied page if authorization fails
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set cookie expiration time
                    options.SlidingExpiration = true; // Enable sliding expiration to refresh the cookie on each request
                });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
