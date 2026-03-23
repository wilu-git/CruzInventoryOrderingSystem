using InventoryOrderingSystem.Models;
using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Service.Customers
{
    public interface ICustomerService
    {
        //Requirements
        //Create Customers
        //Show All Customers
        //Delete Customers

        public Task CreateCustomerAsync(Customer customer);
        public Task<List<Customer>> GetAllCustomersAsync();
        public Task<bool> DeleteCustomerAsync(int customerId);
        public Task<Customer> GetCustomerByIdAsync(int customerId);
        public Task<Customer> GetCustomerByNameAsync(string customerName);
        public Task<bool> CustomerIsActive(int customerId);
        public Task<bool> CustomerExists(string username);

        public Task<LoginResponseModel> LoginCustomer(LoginModel model);
    }
}
