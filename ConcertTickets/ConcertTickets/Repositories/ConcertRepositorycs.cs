using ConcertTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTickets.Repositories
{
    public class ConcertRepository : IConcertRepository
    {
        private readonly ApplicationDbContext _context;

        public ConcertRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Concert entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Concert>> GetAllAsync()
        {
            return await _context.Concerts
                .Include(c => c.TicketOffers)
                .ToListAsync();
        }


        public Task<Concert> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Concert>> GetConcertsWithTicketOffersAsync()
        {
            return await _context.Concerts.Include(c => c.TicketOffers).ToListAsync();
        }

        public Task GetConcertWithTicketOffersAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Concert entity)
        {
            throw new NotImplementedException();
        }
    }
}
