using InventoryOrderingSystem.Helpers;
using InventoryOrderingSystem.Models;
using InventoryOrderingSystem.Models.Database;
using InventoryOrderingSystem.Repository.Admins;

namespace InventoryOrderingSystem.Service.Admins
{
    public class AdminsService : IAdminsService
    {
        private readonly IAdminRepository _repo;
        public AdminsService(IAdminRepository repo)
        {
            _repo = repo;
        }
        public async Task<LoginResponseModel> LoginAdmin(LoginModel model)
        {
            var userData = await _repo.GetAdminUserAsync(model.Username); //we first get the username 
            if (userData == null) //if user is empty LoginReponseModel will return false
            {
                // Return unsuccessful login response if user is not found in the database
                return new LoginResponseModel
                {
                    LoginSuccessful = false
                };
            }

            // Verify the provided password against the stored hashed password
            var isPwMatch = SecurityHelper.VerifyPassword(model.Password, userData.Password);

            // Return login response with user ID and verification result
            return new LoginResponseModel
            {
                UserId = userData.AdministratorId,
                LoginSuccessful = isPwMatch
            };
        }

        public async Task RegisterAdminAsync(RegistrationModel model)
        {
            try
            {
                var newAdmin = new Administrator
                {
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = SecurityHelper.EncryptionEmail(model.Email),
                    Password = SecurityHelper.HashPassword(model.Password)
                };

                await _repo.AddAsync(newAdmin);
            }
            catch
            {
                throw new Exception("An error occurred while registering the admin.");
            }
            
            
        }
    }
}
