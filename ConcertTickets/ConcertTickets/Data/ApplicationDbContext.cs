using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcertTickets.Models
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Concert> Concerts { get; set; }
        public DbSet<TicketOffer> TicketOffers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding Concert data
            builder.Entity<Concert>().HasData(
                new Concert { Id = 1, Artist = "Taylor Swift", Location = "Koning Boudewijn Stadion, Brussel", Date = new DateTime(2025, 3, 15) },
                new Concert { Id = 2, Artist = "Taylor Swift", Location = "Koning Boudewijn Stadion, Brussel", Date = new DateTime(2025, 3, 16) },
                new Concert { Id = 3, Artist = "Charli XCX", Location = "Vorst Nationaal, Brussel", Date = new DateTime(2025, 4, 16) },
                new Concert { Id = 4, Artist = "Compact Disk Dummies", Location = "Ancienne Belgique, Brussel", Date = new DateTime(2025, 4, 26) },
                new Concert { Id = 5, Artist = "Compact Disk Dummies", Location = "Ancienne Belgique, Brussel", Date = new DateTime(2025, 4, 27) },
                new Concert { Id = 6, Artist = "Coldplay", Location = "Sportpaleis, Antwerpen", Date = new DateTime(2025, 5, 7) },
                new Concert { Id = 7, Artist = "Dua Lipa", Location = "Werchter", Date = new DateTime(2025, 6, 18) },
                new Concert { Id = 8, Artist = "Dua Lipa", Location = "Werchter", Date = new DateTime(2025, 6, 19) }
                );

            // Seeding TicketOffers
            builder.Entity<TicketOffer>().HasData(
                new TicketOffer { Id = 1, TicketType = "Golden Circle", NumTickets = 10, Price = 200, ConcertId = 1 },
                new TicketOffer { Id = 2, TicketType = "Standing", NumTickets = 50, Price = 50, ConcertId = 1 },
                new TicketOffer { Id = 3, TicketType = "Seated", NumTickets = 60, Price = 60, ConcertId = 1 },
                new TicketOffer { Id = 4, TicketType = "Golden Circle", NumTickets = 1000, Price = 200, ConcertId = 2 },
                new TicketOffer { Id = 5, TicketType = "Standing", NumTickets = 19000, Price = 50, ConcertId = 2 },
                new TicketOffer { Id = 6, TicketType = "Seated", NumTickets = 20000, Price = 60, ConcertId = 2 },
                new TicketOffer { Id = 7, TicketType = "Golden Circle", NumTickets = 0, Price = 100, ConcertId = 3 },
                new TicketOffer { Id = 8, TicketType = "Standing", NumTickets = 0, Price = 28, ConcertId = 3 },
                new TicketOffer { Id = 9, TicketType = "Seated", NumTickets = 0, Price = 32, ConcertId = 3 },
                new TicketOffer { Id = 10, TicketType = "Standing", NumTickets = 2000, Price = 28, ConcertId = 4 },
                new TicketOffer { Id = 11, TicketType = "Seated", NumTickets = 1800, Price = 32, ConcertId = 4 },
                new TicketOffer { Id = 12, TicketType = "Standing", NumTickets = 2000, Price = 28, ConcertId = 5 },
                new TicketOffer { Id = 13, TicketType = "Seated", NumTickets = 7800, Price = 32, ConcertId = 5 },
                new TicketOffer { Id = 14, TicketType = "Golden Circle", NumTickets = 400, Price = 150, ConcertId = 6 },
                new TicketOffer { Id = 15, TicketType = "Standing", NumTickets = 4000, Price = 65, ConcertId = 6 },
                new TicketOffer { Id = 16, TicketType = "Seated", NumTickets = 4000, Price = 55, ConcertId = 6 },
                new TicketOffer { Id = 17, TicketType = "Golden Circle", NumTickets = 1000, Price = 124, ConcertId = 7 },
                new TicketOffer { Id = 18, TicketType = "Standing", NumTickets = 20000, Price = 67, ConcertId = 7 },
                new TicketOffer { Id = 19, TicketType = "Standing", NumTickets = 2000, Price = 36, ConcertId = 8 },
                new TicketOffer { Id = 20, TicketType = "Seated", NumTickets = 7800, Price = 40, ConcertId = 8 }
            );
        }

    }
}