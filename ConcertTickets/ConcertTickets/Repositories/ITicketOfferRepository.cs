using ConcertTickets.Models;

namespace ConcertTickets.Repositories
{
    public interface ITicketOfferRepository
    {
        Task GetByIdAsync(int ticketOfferId);
        Task<TicketOffer> GetTicketOfferByIdAsync(int id);
        Task<IEnumerable<TicketOffer>> GetTicketOffersByConcertIdAsync(int concertId);
        Task UpdateTicketAvailabilityAsync(int ticketOfferId, int numberOfTicketsSold);
    }
}
