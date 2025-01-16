using ConcertTickets.Models;
using ConcertTickets.Repositories;
using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public class TicketOfferService : ITicketOfferService
    {
        private readonly ITicketOfferRepository _ticketOfferRepository;

        public TicketOfferService(ITicketOfferRepository ticketOfferRepository)
        {
            _ticketOfferRepository = ticketOfferRepository;
        }

        public async Task<OrderFormViewModel> GetTicketOfferForOrderAsync(int id, bool hasMemberCard)
        {
            var ticketOffer = await _ticketOfferRepository.GetTicketOfferByIdAsync(id);
            if (ticketOffer == null) return null;

            var discount = hasMemberCard ? 0.1 : 0.0; // 10% korting als je een lidkaart hebt
            return new OrderFormViewModel
            {
                TicketOfferId = ticketOffer.Id,
                FinalPrice = ticketOffer.Price * (1 - discount)
            };
        }

        public async Task UpdateTicketOfferAsync(TicketOfferUpdateViewModel model)
        {
            var ticketOffer = await _ticketOfferRepository.GetByIdAsync(model.TicketOfferId);
            if (ticketOffer != null && ticketOffer.NumTickets >= model.TicketsToSubtract)
            {
                ticketOffer.NumTickets -= model.TicketsToSubtract;
                await _ticketOfferRepository.SaveChangesAsync();
            }
        }

    }
}
