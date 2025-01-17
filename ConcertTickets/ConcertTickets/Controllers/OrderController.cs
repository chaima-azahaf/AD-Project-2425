using ConcertTickets.Models;
using ConcertTickets.Services;
using ConcertTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConcertTickets.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly ITicketOfferService _ticketOfferService;
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            UserManager<CustomUser> userManager,
            ITicketOfferService ticketOfferService,
            IOrderService orderService,
            ILogger<OrderController> logger)
        {
            _userManager = userManager;
            _ticketOfferService = ticketOfferService;
            _orderService = orderService;
            _logger = logger;
        }

        // GET: Order/Create/5
        public async Task<IActionResult> Create(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _logger.LogWarning("User not found while accessing Create");
                    return RedirectToAction("Login", "Account");
                }

                var ticketOffer = await _ticketOfferService.GetTicketOfferByIdAsync(id);
                if (ticketOffer == null)
                {
                    _logger.LogWarning($"TicketOffer not found for ID: {id}");
                    return NotFound();
                }

                var viewModel = new OrderFormViewModel
                {
                    TicketOfferId = ticketOffer.Id,
                    TicketType = ticketOffer.TicketType,
                    FinalPrice = (double)ticketOffer.Price,
                    AvailableTickets = ticketOffer.AvailableTickets,
                    ConcertName = ticketOffer.ConcertName,
                    ConcertDate = ticketOffer.ConcertDate,
                    ConcertLocation = ticketOffer.ConcertLocation,
                    UserId = user.Id,
                    UserName = user.UserName,
                    HasMemberCard = user.HasMemberCard
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create GET action");
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid ModelState in Create POST action");
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            _logger.LogWarning($"Validation Error: {error.ErrorMessage}");
                        }
                    }
                    return View(model);
                }

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _logger.LogWarning("User not found in Create POST action");
                    return RedirectToAction("Login", "Account");
                }

                // Verify ticket offer still exists and has enough tickets
                var ticketOffer = await _ticketOfferService.GetTicketOfferByIdAsync(model.TicketOfferId);
                if (ticketOffer == null)
                {
                    ModelState.AddModelError("", "Deze tickets zijn niet meer beschikbaar.");
                    return View(model);
                }

                if (ticketOffer.AvailableTickets < model.NumberOfTickets)
                {
                    ModelState.AddModelError("NumberOfTickets", "Er zijn niet genoeg tickets beschikbaar.");
                    return View(model);
                }

                // Create the order
                var orderId = await _orderService.CreateOrderAsync(model);

                // Update available tickets
                await _ticketOfferService.UpdateTicketOfferAsync(new TicketOfferUpdateViewModel
                {
                    TicketOfferId = model.TicketOfferId,
                    NumberOfTickets = model.NumberOfTickets
                });

                return RedirectToAction(nameof(Success), new { id = orderId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create POST action");
                ModelState.AddModelError("", "Er is een fout opgetreden bij het verwerken van je bestelling. Probeer het later opnieuw.");
                return View(model);
            }
        }

        // GET: Order/Success/5
        public async Task<IActionResult> Success(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                var viewModel = new OrderSuccessViewModel
                {
                    OrderId = order.Id,
                    ConcertName = order.ConcertName,
                    ConcertDate = order.ConcertDate,
                    TicketType = order.TicketType,
                    NumberOfTickets = order.NumberOfTickets,
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Success action");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}