using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Repository.Products
{
    public interface IProductRepository
    {
        //Create
        public Task AddAsync(Product product);
        //Read
        public Task<List<Product>> GetAllAsync();
        //Update
        public Task UpdateAsync(Product product);
        //Delete
        public Task DeleteAsync(int productId);
    }
}
