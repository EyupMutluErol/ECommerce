using System.ComponentModel.DataAnnotations;

namespace ECommerce.MvcUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
