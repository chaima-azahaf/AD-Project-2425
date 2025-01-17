using ConcertTickets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTickets.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;

        public AdminController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Orders Action
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetUnpaidOrdersAsync(); // Assurez-vous que cette méthode existe
            return View(orders);
        }

        // SetPaid Action
        [HttpPost]
        public async Task<IActionResult> SetPaid(int id)
        {
            await _orderService.SetOrderPaidAsync(id, true); // Implémentez cette méthode dans le service
            return RedirectToAction(nameof(Orders));
        }
    }
}
