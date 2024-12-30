using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public interface ITicketOfferService
    {
        Task<OrderFormViewModel> GetTicketOfferForOrderAsync(int id, bool hasMemberCard);
        Task UpdateTicketOfferAsync(TicketOfferUpdateViewModel model);
    }
}
