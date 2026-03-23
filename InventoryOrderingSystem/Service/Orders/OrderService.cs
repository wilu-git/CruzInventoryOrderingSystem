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

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception($"Order with ID {orderId} not found.");
            }
            return order;
        }

        public Task UpdateOrderAsync(Order order)
        {    
            return _orderRepository.UpdateAsync(order);
        }

        public async Task<int> OrderCountAsync()
        {
            var orderCount = await _orderRepository.GetCountAsync();

            return orderCount;

        }

        public async Task<List<Order>> GetAllOrdersAsync(int page, int pageSize)
        {
            return await _orderRepository.GetAllOrdersAsync(page, pageSize);

        }
    }
}
