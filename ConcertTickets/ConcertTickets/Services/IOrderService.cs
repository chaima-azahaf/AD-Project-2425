using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(OrderFormViewModel model);
        OrderViewModel GetOrderById(int orderId);  // Voor Success-pagina
    }

}
