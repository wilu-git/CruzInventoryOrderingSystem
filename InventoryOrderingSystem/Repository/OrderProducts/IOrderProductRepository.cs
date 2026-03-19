using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Repository.OrderProducts
{
    public interface IOrderProductRepository
    {
            //Create
            public Task AddAsync(OrderProduct orderProduct);
    
            //Read
            public Task<List<OrderProduct>> GetAllAsync();
    
            //Update
            public Task UpdateAsync(OrderProduct orderProduct);

            public Task DeleteAsync(int orderProductId);
    }
}
