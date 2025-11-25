using Microsoft.AspNetCore.Identity;

namespace ECommerce.MvcUI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public DateTime Birthday { get; set; }

    }
}
