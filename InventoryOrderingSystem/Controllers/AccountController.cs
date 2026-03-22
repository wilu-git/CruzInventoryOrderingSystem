using InventoryOrderingSystem.Models;
using InventoryOrderingSystem.Models.Database;
using InventoryOrderingSystem.Repository.Customers;
using InventoryOrderingSystem.Service.Customers;
using Microsoft.AspNetCore.Mvc;

namespace InventoryOrderingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        public AccountController(ICustomerRepository customerRepo)
        {
            _customerService = new CustomerService(customerRepo);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                //I want to create the Customer
            }
            return View(model);
        }
    }
}
