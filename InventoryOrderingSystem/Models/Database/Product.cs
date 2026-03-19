using System;
using System.Collections.Generic;

namespace InventoryOrderingSystem.Models.Database;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Stock { get; set; }

    public string Unit { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
