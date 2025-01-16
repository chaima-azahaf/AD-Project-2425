using ConcertTickets.Models;
using ConcertTickets.Services;
using ConcertTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTickets.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly ITicketOfferService _ticketOfferService;
        private readonly IOrderService _orderService;

        public OrderController(
            UserManager<CustomUser> userManager,
            ITicketOfferService ticketOfferService,
            IOrderService orderService)
        {
            _userManager = userManager;
            _ticketOfferService = ticketOfferService;
            _orderService = orderService;
        }

        // GET: Order/Create
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var hasMemberCard = user.HasMemberCard;
            var ticketOfferViewModel = await _ticketOfferService.GetTicketOfferByIdAsync(id);

            if (ticketOfferViewModel == null) return NotFound();

            var orderFormViewModel = new OrderFormViewModel
            {
                TicketOfferId = ticketOfferViewModel.TicketOfferId,
                FinalPrice = ticketOfferViewModel.FinalPrice,
                AvailableTickets = ticketOfferViewModel.AvailableTickets,
                ConcertName = ticketOfferViewModel.ConcertName,
                ConcertDate = ticketOfferViewModel.ConcertDate,
                UserId = user.Id,
                UserName = user.UserName
            };

            return View(orderFormViewModel);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If the form is invalid, return the view with the same model
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            model.UserId = user.Id;
            model.UserName = user.UserName;

            // Create the order
            var orderId = await _orderService.CreateOrderAsync(model);

            // Update the ticket offer
            var updateModel = new TicketOfferUpdateViewModel
            {
                TicketOfferId = model.TicketOfferId,
                NumberOfTickets = model.NumberOfTickets
            };
            await _ticketOfferService.UpdateTicketOfferAsync(updateModel);

            return RedirectToAction("Success", new { id = orderId });
        }

        // GET: Order/Success
        [HttpGet]
        public async Task<IActionResult> Success(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            var viewModel = new OrderViewModel
            {
                ConcertName = order.ConcertName,
                ConcertDate = order.ConcertDate,
                TicketType = order.TicketType,
                TotalPrice = order.TotalPrice,
                NumberOfTickets = order.NumberOfTickets,
                UserName = order.UserName,
                Paid = order.Paid,
                HasMemberCard = order.HasMemberCard
            };

            return View(viewModel);
        }
    }
}