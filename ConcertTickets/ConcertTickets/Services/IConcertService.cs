using ConcertTickets.Models;
using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public interface IConcertService
    {
        Task<IEnumerable<ConcertViewModel>> GetAllConcertsAsync();
        Task<ConcertViewModel> GetConcertByIdAsync(int id);
        Task<ConcertViewModel> GetConcertWithTicketOffersByIdAsync(int id);
    }
}
