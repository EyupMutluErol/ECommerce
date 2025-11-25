using ECommerce.MvcUI.Identity;
using ECommerce.MvcUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{
   
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminRoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesUsers = new Dictionary<string, List<string>>();

            foreach (var role in roles)
            {
                 var userInRole = new List<string>();
                foreach (var user in _userManager.Users)
                {
                    if ( await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userInRole.Add( user.UserName);
                    }
                }
                rolesUsers[role.Name] = userInRole;
            }
            ViewBag.RolesUsers = rolesUsers;
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (string roleName)
        {
            if (ModelState.IsValid) // model dogralandıysa
            {
                var result=await _roleManager.CreateAsync( new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(roleName);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
  
            IdentityRole role= await _roleManager.FindByIdAsync(id);

            if (role==null)
            {
                TempData["message"]="Role not found";
                return RedirectToAction("Index");
            }

            var members = new List<ApplicationUser>();
            var nonMembers = new List<ApplicationUser>();
            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            var model = new RoleDetails
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditModel roleEditModel)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {

                var normalized = roleEditModel.RoleName.ToUpperInvariant();
                var role = _roleManager.Roles.FirstOrDefault(x => x.NormalizedName == normalized);
                if (role==null)
                {
                    ModelState.AddModelError("", $"Role {roleEditModel.RoleName} not found");
                        return RedirectToAction("Index");
                }

                foreach ( var userId in roleEditModel.IdsToAdd ?? new string[] {})
                {
                    var user= await _userManager.FindByIdAsync(userId);
                    if (user!=null)
                    {
               
                        result = await _userManager.AddToRoleAsync(user, roleEditModel.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in roleEditModel.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, roleEditModel.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return  RedirectToAction("Edit",roleEditModel.RoleId);
                }
            }

            return RedirectToAction("Index");

        }

    }
}
