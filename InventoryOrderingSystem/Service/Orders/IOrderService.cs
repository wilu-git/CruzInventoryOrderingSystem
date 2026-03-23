using InventoryOrderingSystem.Models.Database;
using System.Runtime.CompilerServices;

namespace InventoryOrderingSystem.Service.Orders
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync(int page, int pageSize);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<int> OrderCountAsync();
        
    }
}
