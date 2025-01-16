using ConcertTickets.Repositories;
using ConcertTickets.ViewModels;
using ConcertTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTickets.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _concertRepository;
        private readonly ApplicationDbContext _context;
        public ConcertService(IConcertRepository concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public Task<IEnumerable<ConcertViewModel>> GetAllConcertsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ConcertViewModel> GetConcertByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ConcertViewModel> GetConcertWithTicketOffersByIdAsync(int id)
        {
            var concert = await _context.Concerts
                .Include(c => c.TicketOffers)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (concert == null)
            {
                return null;
            }

            return new ConcertViewModel
            {
                Id = concert.Id,
                Artist = concert.Artist,
                Location = concert.Location,
                Date = concert.Date,
                ArtistPicture = $"/images/{concert.Artist.Replace(" ", string.Empty)}.jpg",
                TicketOffers = concert.TicketOffers.Select(to => new TicketOfferViewModel
                {
                    Id = to.Id,
                    TicketType = to.TicketType,
                    Price = (decimal)to.Price,
                    AvailableTickets = to.NumTickets
                }).ToList()
            };
        }
    }
}
