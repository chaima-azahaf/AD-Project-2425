using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public interface ITicketOfferService
    {
        Task<TicketOfferViewModel> GetTicketOfferByIdAsync(int id);
        string? GetTicketOfferForOrder(int id, bool hasMemberCard);
        Task<OrderFormViewModel> GetTicketOfferForOrderAsync(int id, bool hasMemberCard);
        Task<IEnumerable<TicketOfferViewModel>> GetTicketOffersByConcertIdAsync(int concertId);
        Task UpdateTicketAvailabilityAsync(TicketOfferUpdateViewModel model);
        Task UpdateTicketOfferAsync(TicketOfferUpdateViewModel ticketUpdateModel);
        

    }
}
