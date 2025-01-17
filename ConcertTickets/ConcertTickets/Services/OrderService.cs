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
                TicketOfferId = model.TicketOfferId,
                NumberOfTickets = model.NumberOfTickets,
                TotalPrice = model.FinalPrice,
                Paid = false
            };

            return await _orderRepository.CreateOrderAsync(order);
        }

        public Task GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null) return null;

            return new OrderViewModel
            {
                OrderId = order.Id,
                ConcertName = order.TicketOffer.Concert.Artist,
                TicketType = order.TicketOffer.TicketType,
                TotalPrice = (decimal)order.TotalPrice,
                UserName = order.UserName,
                Paid = order.Paid
            };
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersByStatusAsync(bool paid)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(paid);

            return orders.Select(order => new OrderViewModel
            {
                OrderId = order.Id,
                ConcertName = order.TicketOffer.Concert.Artist,
                TicketType = order.TicketOffer.TicketType,
                TotalPrice = (decimal)order.TotalPrice,
                UserName = order.UserName,
                Paid = order.Paid
            });
        }

        public async Task UpdateOrderPaidStatusAsync(int orderId, bool paid)
        {
            await _orderRepository.UpdateOrderPaidStatusAsync(orderId, paid);
        }

        //admin
        public async Task<IEnumerable<OrderViewModel>> GetUnpaidOrdersAsync()
        {
            var orders = await _orderRepository.GetUnpaidOrdersAsync();
            return orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                ConcertName = o.TicketOffer.Concert.Artist,
                ConcertDate = (DateTime)o.ConcertDate,
                TicketType = o.TicketType,
                NumberOfTickets = o.NumberOfTickets,
                TotalPrice = (decimal)o.TotalPrice,
                HasMemberCard = o.HasMemberCard,
                UserName = o.UserName,
                Paid = o.Paid
            });
        }

        public async Task SetOrderPaidAsync(int orderId, bool isPaid)
        {
            await _orderRepository.UpdateOrderPaidStatusAsync(orderId, isPaid);
        }

    }
}
