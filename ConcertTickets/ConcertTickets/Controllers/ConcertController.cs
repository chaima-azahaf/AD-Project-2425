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
    }
}
