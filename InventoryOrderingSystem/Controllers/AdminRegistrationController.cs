using InventoryOrderingSystem.Models;
using InventoryOrderingSystem.Repository.Admins;
using InventoryOrderingSystem.Service.Admins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryOrderingSystem.Controllers
{
    public class AdminRegistrationController : Controller
    {
        //Constructor for admin registration repo
        private readonly IAdminsService _adminService;
        public AdminRegistrationController(IAdminRepository adminRepository)
        {
            _adminService = new AdminsService(adminRepository);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                //Process registration data of admin (save it to database) 
                try
                {
                    await _adminService.RegisterAdminAsync(model);
                    ViewBag.SuccessMessage = "Admin registered successfully!";
                }
                catch (Exception ex)
                {
                    //TODO: make a exception make from service layer 
                    ViewBag.ErrorMessage = ex.Message;
                }

            }

            return View(model);
        }
    }
}
