using ConcertTickets.Models;

namespace ConcertTickets.Repositories
{
    public class TicketOfferRepository : Repository<TicketOffer>, ITicketOfferRepository
    {
        public TicketOfferRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TicketOffer> GetTicketOfferByIdAsync(int id)
        {
            return await _context.TicketOffers.FindAsync(id);
        }
    }
}
