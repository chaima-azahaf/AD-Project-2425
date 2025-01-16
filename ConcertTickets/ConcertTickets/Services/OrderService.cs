using ConcertTickets.Models;
using ConcertTickets.Repositories;
using ConcertTickets.ViewModels;

namespace ConcertTickets.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> CreateOrderAsync(OrderFormViewModel model)
        {
            var order = new Order
            {
                UserId = model.UserId,
                UserName = model.UserName,
                ConcertId = model.ConcertId,
                TicketType = model.TicketType,
                NumberOfTickets = model.NumberOfTickets,
                TotalPrice = model.NumberOfTickets * model.Price,
                Paid = false
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
            return order.Id;
        }


        public async Task<IEnumerable<OrderViewModel>> GetOrdersByStatusAsync(bool paid)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(paid);
            return orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                UserId = o.UserId,
                Paid = o.Paid
            });
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order == null ? null : new OrderViewModel { Id = order.Id, Paid = order.Paid };
        }

        public async Task UpdatePaidStatusAsync(int orderId, bool paid)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.Paid = paid;
                await _orderRepository.UpdateAsync(order);
            }
        }
    }
}

