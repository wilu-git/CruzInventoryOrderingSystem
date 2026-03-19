using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Repository.Orders
{
    public interface IOrderRepository
    {
        //Create
        public Task AddAsync(Order order);
        //Read
        public Task<List<Order>> GetAllAsync();
        //Update
        public Task UpdateAsync(Order order);
        //Delete
        public Task DeleteAsync(int orderId);
    }
}
