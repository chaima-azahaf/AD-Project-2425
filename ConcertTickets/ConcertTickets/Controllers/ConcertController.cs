using Microsoft.AspNetCore.Mvc;
using ConcertTickets.Services;

namespace ConcertTickets.Controllers
{
    public class ConcertController : Controller
    {
        private readonly IConcertService _concertService;

        public ConcertController(IConcertService concertService)
        {
            _concertService = concertService;
        }

        public async Task<IActionResult> Index()
        {
            var concerts = await _concertService.GetAllConcertsAsync();
            return View(concerts);
        }

        public async Task<IActionResult> Buy(int id)
        {
            var concert = await _concertService.GetConcertWithTicketOffersByIdAsync(id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }
    }
}
