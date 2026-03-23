using InventoryOrderingSystem.Helpers;
using InventoryOrderingSystem.Models;
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

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task<bool> CustomerExists(string username)
        {
            return await _customerRepository.CustomerExists(username);
        }

        public async Task<bool> CustomerIsActive(int customerId)
        {
            Customer customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            if (customer.IsActive == false)
            {
                return false;
            }

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
            var customer = _customerRepository.GetByIdAsync(customerId); 
            
            if(customer == null)
            {
                throw new Exception($"Customer with ID {customerId} not found.");
            }
            
            return customer;
        }

        public async Task<Customer> GetCustomerByNameAsync(string customerName)
        {
            var customer = await _customerRepository.GetByNameAsync(customerName);
            if (customer == null)
            {
                throw new Exception($"Customer with name {customerName} not found.");
            }   
            return customer;
        }

        public async Task<LoginResponseModel> LoginCustomer(LoginModel model)
        {
            var userData = await _customerRepository.GetByNameAsync(model.Username); //we first get the username
            if(userData == null)
            {
                return new LoginResponseModel
                {
                    LoginSuccessful = false,
                    UserId = 0,
                    IsAdmin = false
                };   
            }
            //verify password using SecurityHelper, which will hash the entered password and compare it to the stored hash
            var isPwMatch = SecurityHelper.VerifyPassword(model.Password, userData.Password);

            //if the password doesn't match, we return a response indicating the login was unsuccessful
            return new LoginResponseModel
            {
                LoginSuccessful = isPwMatch,
                UserId = userData.CustomerId,
                IsAdmin = false
            };

        }
    }
}
