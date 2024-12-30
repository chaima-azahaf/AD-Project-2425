using ConcertTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTickets.Repositories
{
    public class ConcertRepository : Repository<Concert>, IConcertRepository
    {
        public ConcertRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Concert>> GetConcertsWithTicketOffersAsync()
        {
            return await _context.Concerts.Include(c => c.TicketOffers).ToListAsync();
        }

        public async Task<Concert> GetConcertWithTicketOffersAsync(int id)
        {
            return await _context.Concerts.Include(c => c.TicketOffers).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
