using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Models
{
    public class OrderViewModel
    {
        //Applying the pagination in the model
        public List<Order>? Orders { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
