using ConcertTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTickets.Repositories
{
    public class TicketOfferRepository : ITicketOfferRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketOfferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task GetByIdAsync(int ticketOfferId)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketOffer> GetTicketOfferByIdAsync(int id)
        {
            return await _context.TicketOffers
                .Include(t => t.Concert)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TicketOffer>> GetTicketOffersByConcertIdAsync(int concertId)
        {
            return await _context.TicketOffers
                .Where(t => t.ConcertId == concertId)
                .ToListAsync();
        }

        public async Task UpdateTicketAvailabilityAsync(int id, int quantity)
        {
            var ticketOffer = await _context.TicketOffers.FirstOrDefaultAsync(t => t.Id == id);
            if (ticketOffer != null)
            {
                ticketOffer.NumTickets -= quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
