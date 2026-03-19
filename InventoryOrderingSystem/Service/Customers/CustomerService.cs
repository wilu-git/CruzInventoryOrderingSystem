using InventoryOrderingSystem.Models.Database;
using InventoryOrderingSystem.Repository.Customers;

namespace InventoryOrderingSystem.Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository; 
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteAsync(customerId);
            return true;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers;
        }

        public Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var customer = _customerRepository.
        }

        public Task<Customer> GetCustomerByNameAsync(string customerName)
        {
            throw new NotImplementedException();
        }
    }
}
