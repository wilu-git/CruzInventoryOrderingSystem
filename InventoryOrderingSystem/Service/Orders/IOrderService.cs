using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Service.Orders
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}
