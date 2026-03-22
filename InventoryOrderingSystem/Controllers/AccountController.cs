using InventoryOrderingSystem.Models;
using InventoryOrderingSystem.Models.Database;
using InventoryOrderingSystem.Repository.Admins;
using InventoryOrderingSystem.Repository.Customers;
using InventoryOrderingSystem.Service.Admins;
using InventoryOrderingSystem.Service.Customers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryOrderingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAdminsService _adminService;
        public AccountController(ICustomerRepository customerRepo, IAdminRepository adminRepo)
        {
            _adminService = new AdminsService(adminRepo);
            _customerService = new CustomerService(customerRepo);
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _adminService.LoginAdmin(model);

                if ((bool)res.LoginSuccessful)
                {
                    //Claims are important to determine who is logged in to the system and also to use the SignInAsync Method                    
                    //to create the cookie for the user that is logged in.
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.NameIdentifier, res.UserId.ToString())
                    };

                    var identity = new ClaimsIdentity(claims,
                       CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);


                    //Redirect to the Home controller and the index action 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Username and password do not match 
                    ViewBag.Error = "Username and password do not match";
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task Logout()
        {

        }
    }
}
