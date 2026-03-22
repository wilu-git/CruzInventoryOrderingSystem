using InventoryOrderingSystem.Models.Database;

namespace InventoryOrderingSystem.Repository.Admins
{
    public class AdminRepository : IAdminRepository
    {
        private readonly InventoryOrderingSystemContext _context;
        public AdminRepository(InventoryOrderingSystemContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
            await _context.SaveChangesAsync();
        }

        public Task<Administrator> GetAdminUserAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
