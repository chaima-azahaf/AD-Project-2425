using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<TicketOfferViewModel> GetTicketOfferByIdAsync(int id)
        {
            var ticketOffer = await _ticketOfferRepository.GetTicketOfferByIdAsync(id);

            if (ticketOffer == null) return null;

            return new TicketOfferViewModel
            {
                TicketOfferId = ticketOffer.Id,
                TicketType = ticketOffer.TicketType,
                Price = (decimal)ticketOffer.Price,
                NumTickets = ticketOffer.NumTickets,
                ConcertName = ticketOffer.Concert.Artist,
                ConcertDate = ticketOffer.Concert.Date
            };
        }

        public Task GetTicketOfferForOrderAsync(int id, bool hasMemberCard)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TicketOfferViewModel>> GetTicketOffersByConcertIdAsync(int concertId)
        {
            var ticketOffers = await _ticketOfferRepository.GetTicketOffersByConcertIdAsync(concertId);

            return ticketOffers.Select(ticketOffer => new TicketOfferViewModel
            {
                TicketOfferId = ticketOffer.Id,
                TicketType = ticketOffer.TicketType,
                Price = (decimal)ticketOffer.Price,
                NumTickets = ticketOffer.NumTickets,
                ConcertName = ticketOffer.Concert.Artist,
                ConcertDate = ticketOffer.Concert.Date
            });
        }

        public async Task UpdateTicketAvailabilityAsync(TicketOfferUpdateViewModel model)
        {
            await _ticketOfferRepository.UpdateTicketAvailabilityAsync(model.TicketOfferId, model.NumberOfTicketsSold);
        }

        public Task UpdateTicketOfferAsync(TicketOfferUpdateViewModel ticketUpdateModel)
        {
            throw new NotImplementedException();
        }
    }
}
