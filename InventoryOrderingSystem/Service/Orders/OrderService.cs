using InventoryOrderingSystem.Models.Database;
using InventoryOrderingSystem.Repository.Orders;

namespace InventoryOrderingSystem.Service.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> CreateOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteAsync(orderId);
            return true;
        }

        public Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = _orderRepository.GetAllAsync();
            return orders;
        }

        public Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = _orderRepository.GetAllAsync().Result.FirstOrDefault(x => x.OrderId == orderId);
            if (order == null)
            {
                throw new Exception($"Order with ID {orderId} not found.");
            }
            return Task.FromResult(order);
        }

        public Task UpdateOrderAsync(Order order)
        {    
            return _orderRepository.UpdateAsync(order);
        }
    }
}
