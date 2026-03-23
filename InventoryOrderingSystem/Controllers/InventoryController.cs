using Microsoft.AspNetCore.Mvc;

namespace InventoryOrderingSystem.Controllers
{
    public class InventoryController : Controller
    {
        //Index, StockIn, StockOut, Adjust, InventoryHistory
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StockIn()
        {
            return View();
        }

        public IActionResult StockOut()
        {
            return View();
        }

        public IActionResult Adjust()
        {
            return View();
        }

        public IActionResult InventoryHistory()
        {
            return View();
        }
    }
}
