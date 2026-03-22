using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Repository.Admins
{
    public interface IAdminRepository
    {
        public Task<Administrator> GetAdminUserAsync(string username);
        public Task AddAsync(Administrator administrator);

    }
}
