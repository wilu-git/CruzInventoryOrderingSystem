namespace InventoryOrderingSystem.Models
{
    public class LoginResponseModel
    {
        public int UserId { get; set; } = 0;
        public bool? LoginSuccessful { get; set; } = false;
        public bool IsAdmin { get; set; } = false;


    }
}
