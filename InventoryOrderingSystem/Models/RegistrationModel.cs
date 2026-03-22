using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryOrderingSystem.Models
{
    public class RegistrationModel
    {
        [Required]
        //Different Error to regular expression wherein it validates the character length
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 20 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers.")]
        [Display(Name ="Username")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Cannot exceed 25 characters.")]
        public string FirstName { get; set; }

        [Required]
        public string LastName  { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Address")] 
        [DisplayName("Email Address")]
        public string Email     { get; set; }
    }
}
