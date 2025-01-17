﻿using System.Collections.Generic;
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

        public Task AddAsync(Order order)
        {
            throw new NotImplementedException();
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
        

        //admin

        public async Task<IEnumerable<Order>> GetUnpaidOrdersAsync()
        {
            return await _context.Orders.Where(o => !o.Paid).ToListAsync();
        }

        public async Task UpdateOrderPaidStatusAsync(int orderId, bool isPaid)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Paid = isPaid;
                await _context.SaveChangesAsync();
            }
        }

    }
}
