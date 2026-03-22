using InventoryOrderingSystem.Models.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryOrderingSystem.Repository.Customers
{
    public interface ICustomerRepository
    {
        //Create
        public Task AddAsync(Customer customer);

        //Read
        public Task<List<Customer>> GetAllAsync();
        public Task<Customer> GetByIdAsync(int customerId);
        public Task<Customer?> GetByNameAsync(string customerName);

        //Update
        public Task UpdateAsync(Customer customer);


        //Delete
        public Task DeleteAsync(int customerId);



    }
}
