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
                AvailableTickets = ticketOffer.NumTickets,
                NumTickets = ticketOffer.NumTickets,
                ConcertName = ticketOffer.Concert.Artist,
                ConcertDate = ticketOffer.Concert.Date
            };
        }

        public async Task<OrderFormViewModel> GetTicketOfferForOrderAsync(int id, bool hasMemberCard)
        {
            var ticketOffer = await _ticketOfferRepository.GetTicketOfferByIdAsync(id);
            if (ticketOffer == null) return null;

            var discount = hasMemberCard ? 0.1M : 0M; // 10% discount for members
            return new OrderFormViewModel
            {
                TicketOfferId = ticketOffer.Id,
                ConcertName = ticketOffer.Concert.Artist,
                ConcertDate = ticketOffer.Concert.Date,
                TicketType = ticketOffer.TicketType,
                FinalPrice = (double)((decimal)ticketOffer.Price * (1 - discount)),
                AvailableTickets = ticketOffer.NumTickets
            };
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

        public OrderFormViewModel GetTicketOfferForOrder(int ticketOfferId, bool hasMemberCard)
        {
            var ticketOffer = _ticketOfferRepository.GetTicketOfferByIdAsync(ticketOfferId).Result;
            if (ticketOffer == null) return null;

            var discount = hasMemberCard ? 0.1 : 0; // Appliquer une remise de 10 % si le membre possède une carte
            return new OrderFormViewModel
            {
                TicketOfferId = ticketOffer.Id,
                TicketType = ticketOffer.TicketType,
                FinalPrice = (double)ticketOffer.Price * (1 - discount),
                AvailableTickets = ticketOffer.NumTickets,
                ConcertName = ticketOffer.Concert.Artist,
                ConcertDate = ticketOffer.Concert.Date,
                ConcertLocation = ticketOffer.Concert.Location,
                DiscountApplied = hasMemberCard
            };
        }

        string? ITicketOfferService.GetTicketOfferForOrder(int id, bool hasMemberCard)
        {
            throw new NotImplementedException();
        }
    }
}
