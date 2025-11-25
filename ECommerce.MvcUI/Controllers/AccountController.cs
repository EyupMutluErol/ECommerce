using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Identity;
using ECommerce.MvcUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ECommerce.MvcUI.EmailServices;
using System.Threading.Tasks;

namespace ECommerce.MvcUI.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;
        private ICartService _cartService;
        private IWebHostEnvironment _webHostEnvironment;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ICartService cartService, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cartService = cartService;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            //resim yükleme işlemi 
            string imagePath = "default-avatar.jpg";
            if (register.Image!=null && register.Image.Length>0)
            {
                //dosya yolunu kontrol et
                //5454ssdsi.jpg
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension= Path.GetExtension(register.Image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("Image", "Only image files (jpg,jpeg,png)");
                    return View(register);
                }
                //dosya boyutu kontrol 
                if (register.Image.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("Image", "File size cannot exceed 5MB");
                    return View(register);

                }
                //guid.jpg
                var fileName = $"{Guid.NewGuid()}{extension}";

                //wwwroot/Images/user klasörü olustur
                var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image", "users");
                Directory.CreateDirectory(uploadFolder);


                var filePath = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await register.Image.CopyToAsync(fileStream);
                }
                imagePath = $"images/users/{fileName}";
            }


            var user = new ApplicationUser
            {
                UserName = register.UserName,
                Email = register.Email,
                FullName = register.FullName,
                PhoneNumber = register.PhoneNumber,
                Image=imagePath,
                Birthday =register.BirthDay ,
                EmailConfirmed=true ,// onaylanmıs mail true set edilecektir. ,
                PhoneNumberConfirmed=true
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
               // await _emailSender.SendEmailAsync(register.Email, "Confirm your account.", $"<a href ='http://localhost:5127{callbackUrl}'>Click on the link to confirm your mail account.</a>");
                return RedirectToAction("Login", "Account");


            }
            if(imagePath!= "default-avatar.jpg")
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            ModelState.AddModelError("", "Your password or email address is incorrect");
            return View(register);
        }

        public IActionResult Login(string ReturnUrl = null)
        {
            ReturnUrl ??=Request.Headers["Referer"].ToString();
            return View(new LoginModel
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "No user has benn created with this email address before");
                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Please confirm your accounnt by email");
                return View(model);

            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");

            }
            ModelState.AddModelError("", "Your password or email address is incorrect");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("~/");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


        //ConfirmEmail










    }
}
