using InventoryOrderingSystem.Models;

namespace InventoryOrderingSystem.Service.Admins
{
    public interface IAdminsService
    {
        Task<LoginResponseModel> LoginAdmin(LoginModel model);
        Task RegisterAdminAsync(RegistrationModel model);
        
    }
}
