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
            //Snippet contains catching if the username Exists Already
            //// Check if username exists in EITHER table
            //var adminExists = await _adminService.UsernameExists(model.Username);
            //var customerExists = await _customerService.UsernameExists(model.Username);

            //if (adminExists || customerExists)
            //{
            //    ModelState.AddModelError("Username", "Username already taken");
            //    return View(model);
            //}
            //// ... proceed with registration
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
                var res = new LoginResponseModel();

                // Attempt to authenticate the user by checking both admin and customer tables.
                // First, check if the username exists in the admin table and attempt admin login.
                if (await _adminService.AdminExists(model.Username))
                {
                    res = await _adminService.LoginAdmin(model);
                } 

                // check if the username exists in the customer table and attempt customer login.
                if (await _customerService.CustomerExists(model.Username))
                {
                    res = await _customerService.LoginCustomer(model);
                }

                if ((bool)res.LoginSuccessful)
                {
                    //Claims are important to determine who is logged in to the system and also to use the SignInAsync Method                    
                    //to create the cookie for the user that is logged in.
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.NameIdentifier, res.UserId.ToString()),
                        // Add role claim based on user type
                        new Claim(ClaimTypes.Role, res.IsAdmin ? "Admin" : "Customer")
                    };
                    

                    var identity = new ClaimsIdentity(claims,
                       CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    //Add ViewBag for successful Login but does not work because Viewbag does not survive when it redirects
                    ViewBag.SuccessMessage = $"Welcome {model.Username}";

                    //We use Tempdata instead But instead of putting it onto login page we now integrate it with home page
                    TempData["SuccessMessage"] = $"Welcome {model.Username}!";

                    if(res.IsAdmin)
                    {
                        //Redirect to the Admin controller and the index action 
                        return RedirectToAction("Index", "Admin");
                    }
                    //Redirect to the Home controller and the index action 
                    return RedirectToAction("Index", "CustomerDashboard");
                }
                else
                {
                    //Username and password do not match 
                    ViewBag.ErrorMessage = "Username and password do not match";
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task <IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
