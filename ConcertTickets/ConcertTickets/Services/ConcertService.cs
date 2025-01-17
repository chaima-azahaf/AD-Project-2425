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
        public ConcertService(IConcertRepository concertRepository, ApplicationDbContext context)
        {
            _concertRepository = concertRepository;
            _context = context;

            //verificatie of context is geinitialiseerd
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized. Ensure it is injected properly.");
            }

        }


        public async Task<IEnumerable<ConcertViewModel>> GetAllConcertsAsync()
        {
            var concerts = await _concertRepository.GetAllAsync();

            return concerts.Select(static c => new ConcertViewModel
            {
                Id = c.Id,
                Artist = c.Artist,
                Location = c.Location,
                Date = c.Date,
                ArtistPicture = $"/img/{c.Artist.Replace(" ", string.Empty)}.png",
                TotalTicketsAvailable = c.TicketOffers?.Sum(to => to.NumTickets) ?? 0
            }).ToList();
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
                ArtistPicture = $"/img/{concert.Artist.Replace(" ", string.Empty)}.png",
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
