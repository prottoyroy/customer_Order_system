using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(int orderId);
        Task<bool> CreateOrderAsync(Order order);
        Task<List<Order>> GetOrdersAsync();
    }
}