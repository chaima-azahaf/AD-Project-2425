using ConcertTickets.Models;
using ConcertTickets.Repositories;
using ConcertTickets.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConcertTickets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Dependency injection voor repositories
            builder.Services.AddScoped<IConcertRepository, ConcertRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ITicketOfferRepository, TicketOfferRepository>();

            // Dependency injection voor services
            builder.Services.AddScoped<IConcertService, ConcertService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ITicketOfferService, TicketOfferService>();

            // Voeg roles en CustomUser toe
            builder.Services.AddDefaultIdentity<CustomUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Concert}/{action=Index}/{id?}");

            // Seeding admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<CustomUser>>();  // Gebruik CustomUser
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await SeedAdminUserAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Er is een fout opgetreden bij het seeden van de database.");
                }
            }

            app.Run();
        }

        private static async Task SeedAdminUserAsync(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await userManager.FindByEmailAsync("admin@concerttickets.com") == null)
            {
                var adminUser = new CustomUser
                {
                    UserName = "admin@concerttickets.com",
                    Email = "admin@concerttickets.com",
                    FirstName = "Admin",
                    LastName = "Gebruiker",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
