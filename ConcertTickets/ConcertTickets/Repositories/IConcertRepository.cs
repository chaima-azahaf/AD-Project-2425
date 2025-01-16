using ConcertTickets.Models;

namespace ConcertTickets.Repositories
{
    public interface IConcertRepository : IRepository<Concert>
    {
        Task<IEnumerable<Concert>> GetConcertsWithTicketOffersAsync();
    }
}

