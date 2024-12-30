using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(OrderFormViewModel model);
        Task<IEnumerable<OrderViewModel>> GetOrdersByStatusAsync(bool paid);
        Task<OrderViewModel> GetOrderByIdAsync(int orderId);
        Task UpdatePaidStatusAsync(int orderId, bool paid);
    }
}
