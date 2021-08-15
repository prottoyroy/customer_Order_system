using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<bool> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            var createdRowCount = await _context.SaveChangesAsync();
            return createdRowCount > 0;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            var order = await _context.Orders.Include( a =>a.ApplicationUser)
            .Include(i => i.OrderItems)
            .ThenInclude(p =>p.Product)
            .SingleOrDefaultAsync(s =>s.OrderId==orderId);
            return order;
                
        }
    }
}