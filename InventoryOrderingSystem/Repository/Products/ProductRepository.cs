using InventoryOrderingSystem.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace InventoryOrderingSystem.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryOrderingSystemContext _context;
        public ProductRepository(InventoryOrderingSystemContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
