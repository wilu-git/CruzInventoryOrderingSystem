using InventoryOrderingSystem.Models;
using InventoryOrderingSystem.Models.Database;
using InventoryOrderingSystem.Repository.Orders;
using InventoryOrderingSystem.Service.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryOrderingSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderService = new OrderService(orderRepository);
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            //TODO: Display all order
            // Hide all completed Orders by default 
            // Changeable Order 
            // add Order 
            // Update order (edit and change number of quantity)
            // Diplays order details when pressed 

            // Validate page number
            if (page < 1) page = 1;

            // Get total count
            var totalItems = await _orderService.OrderCountAsync();

            if (totalItems == 0)
            {
                // Return empty view instead of NotFound
                var emptyModel = new OrderViewModel
                {
                    Orders = new List<Order>(),
                    CurrentPage = 1,
                    PageSize = pageSize,
                    TotalItems = 0,
                    TotalPages = 0
                };
                return View(emptyModel);
            }
            // Calculate total pages
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Ensure page doesn't exceed total pages
            if (page > totalPages) page = totalPages;

            // Get paginated orders
            var orders = await _orderService.GetAllOrdersAsync(page, pageSize);

            // Create view model
            var model = new OrderViewModel
            {
                Orders = orders,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };

            return View(model);
        }
    }
}
