using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Repository.Orders
{
    public interface IOrderRepository
    {
        //Create
        public Task AddAsync(Order order);
        //Read
        public Task<List<Order>> GetAllAsync();
        public Task<Order?> GetByIdAsync(int id);
        public Task<int> GetCountAsync();
        public Task<List<Order>> GetAllOrdersAsync(int page, int pageSize);
        //Update
        public Task UpdateAsync(Order order);
        //Delete
        public Task DeleteAsync(int orderId);


    }
}
