using ECommerce.Business.Abstract;
using ECommerce.Entities;
using ECommerce.MvcUI.Identity;
using ECommerce.MvcUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{

    public class AdminLoginController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;


        public AdminLoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult AdminLogin(string returnUrl = null)
        {
            if (string.IsNullOrEmpty(returnUrl)||!Url.IsLocalUrl(returnUrl ))
            {
                returnUrl = Url.Action("Dashboard", "Admin");
            }
            var model = new LoginModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginModel model) 
        {

            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) 
            {

                ModelState.AddModelError("", "No user has been created with this e-mail address before");

                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Please confirm your account by email");

                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded) 
            {
                if (string.IsNullOrEmpty(model.ReturnUrl) || !Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Dashboard", "Admin");
            }
            ModelState.AddModelError("", "Your password ve email address is incorrect");

            return View(model);

        }




    }
}
