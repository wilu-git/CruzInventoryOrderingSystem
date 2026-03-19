using InventoryOrderingSystem.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace InventoryOrderingSystem.Repository.OrderProducts
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly InventoryOrderingSystemContext _context;

        public OrderProductRepository(InventoryOrderingSystemContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int orderProductId)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(op => op.OrderProductId == orderProductId);
            if (orderProduct != null)
            {
                _context.OrderProducts.Remove(orderProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderProduct>> GetAllAsync()
        {
            return await _context.OrderProducts.ToListAsync();
        }

        public async Task UpdateAsync(OrderProduct orderProduct)
        {
            _context.OrderProducts.Update(orderProduct);
            await _context.SaveChangesAsync();
        }
    }
}

