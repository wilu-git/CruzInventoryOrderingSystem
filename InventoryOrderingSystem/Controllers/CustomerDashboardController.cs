using Microsoft.AspNetCore.Mvc;

namespace InventoryOrderingSystem.Controllers
{
    public class CustomerDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
