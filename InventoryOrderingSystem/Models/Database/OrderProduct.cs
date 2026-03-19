using System;
using System.Collections.Generic;

namespace InventoryOrderingSystem.Models.Database;

public partial class OrderProduct
{
    public int OrderProductId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal SubTotal { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
