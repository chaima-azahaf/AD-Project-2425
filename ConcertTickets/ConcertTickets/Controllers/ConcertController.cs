using ConcertTickets.Services;
using ConcertTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTickets.Controllers
{
    public class ConcertController : Controller
    {
        private readonly IConcertService _concertService;

        // Constructor injecteert de service
        public ConcertController(IConcertService concertService)
        {
            _concertService = concertService;
        }

        // Actie voor Concert > Index
        [HttpGet]
        public IActionResult Index()
        {
            var concerten = _concertService.GetAllConcerts();  // Haal alle concerten op via de service
            return View(concerten);
        }

        [HttpGet]
        public IActionResult Buy(int id)
        {
            var concert = _concertService.GetConcertById(id);  // Haal het concert op met het specifieke ID
            if (concert == null)
            {
                return NotFound();  // Als het concert niet bestaat, toon een 404-pagina
            }
            return View(concert);  // Geef het concert door aan de view
        }

    }
}
