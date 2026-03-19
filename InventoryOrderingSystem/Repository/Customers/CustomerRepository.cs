using InventoryOrderingSystem.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace InventoryOrderingSystem.Repository.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        //Constructor DI 
        public readonly InventoryOrderingSystemContext _context;

        public CustomerRepository(InventoryOrderingSystemContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            return customer;
        }

        public async Task<Customer> GetByNameAsync(string customerName)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == customerName || x.LastName == customerName);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
