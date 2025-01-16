using ConcertTickets.Models;
using ConcertTickets.Services;
using ConcertTickets.Services.Interfaces;
using ConcertTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]  // Alleen ingelogde gebruikers kunnen bestellen
public class OrderController : Controller
{
    private readonly UserManager<CustomUser> _userManager;
    private readonly ITicketOfferService _ticketOfferService;
    private readonly IOrderService _orderService;

    public OrderController(UserManager<CustomUser> userManager, ITicketOfferService ticketOfferService, IOrderService orderService)
    {
        _userManager = userManager;
        _ticketOfferService = ticketOfferService;
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(int concertId, string ticketType)
    {
        // Haal de ingelogde gebruiker op
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");  // Terugsturen naar loginpagina als niet ingelogd
        }

        // Haal ticketinformatie op via de service
        var ticketOffer = await _ticketOfferService.GetTicketOfferByConcertAndTypeAsync(concertId, ticketType);
        if (ticketOffer == null)
        {
            return NotFound();  // Als het tickettype niet bestaat
        }

        // Vul het OrderFormViewModel
        var model = new OrderFormViewModel
        {
            ConcertId = concertId,
            TicketType = ticketType,
            Price = user.HasMemberCard ? ticketOffer.Price * 0.9m : ticketOffer.Price,  // 10% korting voor lidkaarthouders
            AvailableTickets = ticketOffer.NumTickets,
            UserId = user.Id,
            UserName = user.UserName
        };

        return View(model);  // Toon de bestelpagina
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]  // Alleen ingelogde gebruikers kunnen bestellen
    public async Task<IActionResult> Create(OrderFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);  // Toon opnieuw de pagina als het formulier niet valide is
        }

        // Haal de ingelogde gebruiker op
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Vul de ontbrekende gegevens in het model
        model.UserId = user.Id;
        model.UserName = user.UserName;

        // Maak een nieuwe order via de order service
        var orderId = await _orderService.CreateOrderAsync(model);

        // Update het aantal beschikbare tickets
        var updateModel = new TicketOfferUpdateViewModel
        {
            TicketOfferId = model.ConcertId,  // Id van het TicketOffer
            TicketsToSubtract = model.NumberOfTickets
        };

        await _ticketOfferService.UpdateTicketOfferAsync(updateModel);

        // Redirect naar Success-pagina met de orderId
        return RedirectToAction("Success", "Order", new { id = orderId });
    }

    [HttpGet]
    public IActionResult Success(int id)
    {
        var order = _orderService.GetOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

}
