using InventoryOrderingSystem.Models.Database;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> AdminExists(string username)
        {
            return await _context.Administrators.AnyAsync(x => x.Username == username);
        }

        public async Task<Administrator?> GetAdminUserAsync(string username)
        {
            var admin = await _context.Administrators.FirstOrDefaultAsync(x => x.Username == username);
            return admin;
        }
    }
}
