using System;
using System.Collections.Generic;

namespace InventoryOrderingSystem.Models.Database;

public partial class Administrator
{
    public int AdministratorId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
}
