using Microsoft.AspNetCore.Identity;

namespace ECommerce.MvcUI.Models
{
    public class RoleDetails
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<IdentityUser> Members { get; set; }
        public IEnumerable<IdentityUser> NonMembers { get; set; }

    }
    public class RoleEditModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string[]? IdsToAdd { get; set; }
        public string[]? IdsToDelete { get; set; }
        // IdsToDelete burası nullable olmalıdır model ilkkez role eklediginde ModelState hata verir dolayısıyla 
        //burayı nullable yaparak hatanın onune geçeriz burada amacımız roller tablosunda edit edecegi
        //bir id bulamadıgı icin hataya dusmektedir. 
    }
}
