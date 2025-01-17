using ConcertTickets.Models;
using ConcertTickets.Repositories;
using ConcertTickets.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConcertTickets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Dependency injection pour repositories et services
            builder.Services.AddScoped<IConcertRepository, ConcertRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ITicketOfferRepository, TicketOfferRepository>();

            builder.Services.AddScoped<IConcertService, ConcertService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ITicketOfferService, TicketOfferService>();

            // Configuration pour Identity
            builder.Services.AddDefaultIdentity<CustomUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireClaim("IsAdmin", "true")); // Policy vereist dat de gebruiker de claim "IsAdmin" heeft met de waarde "true".

            });

            var app = builder.Build();
            SeedClaimsAsync(app.Services).GetAwaiter().GetResult();
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
                pattern: "{controller=Concert}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }

        static async Task SeedClaimsAsync(IServiceProvider serviceProvider)
        {
            const string ADMIN_ACCOUNT = "admin@test.be";
            const string ADMIN_PASSWORD = "Admin@123";
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();

            var user = await userManager.FindByEmailAsync(ADMIN_ACCOUNT);
            if (user == null)
            {
                // Optioneel: maak een standaardgebruiker aan als die niet bestaat
                user = new CustomUser
                {
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = ADMIN_ACCOUNT,
                    Email = ADMIN_ACCOUNT,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, ADMIN_PASSWORD); // Standaard wachtwoord
            }

            // Voeg claims toe aan de gebruiker
            var claims = new[]
            {
                new Claim("IsAdmin", "true"),
            };

            foreach (var claim in claims)
            {
                if (!(await userManager.GetClaimsAsync(user)).Any(c => c.Type == claim.Type))
                {
                    await userManager.AddClaimAsync(user, claim);
                }
            }
        }

    }
}
