using InventoryOrderingSystem.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace InventoryOrderingSystem.Repository.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InventoryOrderingSystemContext _context;
        public OrderRepository(InventoryOrderingSystemContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order); 
            await _context.SaveChangesAsync();  
        }

        public async Task DeleteAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            return _context.SaveChangesAsync(); 
        }
    }
}
