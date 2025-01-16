using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTickets.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.TicketOffer)
                .ThenInclude(t => t.Concert)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(bool paid)
        {
            return await _context.Orders
                .Where(o => o.Paid == paid)
                .ToListAsync();
        }

        public async Task UpdateOrderPaidStatusAsync(int orderId, bool paid)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Paid = paid;
                await _context.SaveChangesAsync();
            }
        }
    }
}
