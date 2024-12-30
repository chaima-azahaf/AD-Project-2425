using ConcertTickets.Models;
using ConcertTickets.Repositories;
using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _concertRepository;

        public ConcertService(IConcertRepository concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public async Task<IEnumerable<ConcertViewModel>> GetAllConcertsAsync()
        {
            var concerts = await _concertRepository.GetConcertsWithTicketOffersAsync();
            return concerts.Select(c => new ConcertViewModel
            {
                Id = c.Id,
                Artist = c.Artist,
                Date = c.Date,
                TicketOffers = c.TicketOffers.Select(t => new TicketOfferViewModel
                {
                    Price = t.Price,
                    Available = t.Available
                }).ToList()
            });
        }

        public async Task<ConcertViewModel> GetConcertByIdAsync(int id)
        {
            var concert = await _concertRepository.GetConcertWithTicketOffersAsync(id);
            if (concert == null) return null;

            return new ConcertViewModel
            {
                Id = concert.Id,
                Artist = concert.Artist,
                Date = concert.Date,
                TicketOffers = concert.TicketOffers.Select(t => new TicketOfferViewModel
                {
                    Price = t.Price,
                    Available = t.Available
                }).ToList()
            };
        }
    }
}
