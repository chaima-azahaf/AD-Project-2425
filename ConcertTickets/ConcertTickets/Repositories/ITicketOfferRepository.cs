using ConcertTickets.Models;

namespace ConcertTickets.Repositories
{
    public interface ITicketOfferRepository : IRepository<TicketOffer>
    {
        Task<TicketOffer> GetTicketOfferByIdAsync(int id);
    }
}

