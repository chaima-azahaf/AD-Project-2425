using ConcertTickets.Models;

namespace ConcertTickets.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(Order order); 
        Task<Order> GetOrderByIdAsync(int id); 
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(bool paid);

        //admin
        Task<IEnumerable<Order>> GetUnpaidOrdersAsync();
        Task UpdateOrderPaidStatusAsync(int orderId, bool paid);

    }
}
