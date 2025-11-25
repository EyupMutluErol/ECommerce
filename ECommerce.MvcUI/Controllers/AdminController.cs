using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Identity;
using ECommerce.MvcUI.Models.ProductModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IProductService _productService;

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ICampaingService _campaingService;
        public AdminController(IProductService productService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICampaingService campaingService)
        {
            _productService = productService;
            _userManager = userManager;
            _signInManager = signInManager;
            _campaingService = campaingService;
        }


        public IActionResult Dashboard()
        {

            var values = _productService.GetAll().Count().ToString();
            ViewBag.v1 = values;
            var values2 = _productService.GetAll().Select(x => x.Brand).Distinct().Count().ToString();
            ViewBag.v2 = values2;
            var values3 = _productService.GetAll().Count(x => x.StockQuantity < 60).ToString();
            ViewBag.v3 = values3;
            var values4 = _userManager.Users.Count().ToString();
            ViewBag.v4 = values4;
            return View();
        }

        public IActionResult HomeMonth()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll()
            });
        }

        [HttpGet] // request sonucunda bizlere dbden almıs oldugu datayı getirir ve gösterir. 
        public IActionResult UpdateMonth(int id)
        {
            var entity = _productService.GetById(id);

            return View(new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                HomeMonth = entity.ShowOnPageAsMonthlyHighlight

            });
        }
        [HttpPost] // request sonucunda bizden gen datayı alır ve db ye gönderir.
        public IActionResult UpdateMonth(ProductModel model)
        {
            var entity = _productService.GetById(model.Id);
            if (entity==null)
            {
                return NotFound();// bulunamadı 
            }
            entity.ShowOnPageAsMonthlyHighlight = model.HomeMonth;
            _productService.Update(entity, null);
            return RedirectToAction("HomeMonth");
        }
        public IActionResult HomeToday()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll()
            });
        }
        [HttpGet] // request sonucunda bizlere dbden almıs oldugu datayı getirir ve gösterir. 
        public IActionResult UpdateToday(int id)
        {
            var entity = _productService.GetById(id);

            return View(new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                //HomeToday = entity.Today,

            });
        }
        [HttpPost] // request sonucunda bizden gen datayı alır ve db ye gönderir.
        public IActionResult UpdateToday(ProductModel model)
        {
            var entity = _productService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();// bulunamadı 
            }
            //HomeToday = entity.Today,
            _productService.Update(entity, null);
            return RedirectToAction("HomeMonth");
        }



        public IActionResult HomePopular()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll()
            });
        }
        [HttpGet] // request sonucunda bizlere dbden almıs oldugu datayı getirir ve gösterir. 
        public IActionResult UpdatePopular(int id)
        {
            var entity = _productService.GetById(id);

            return View(new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                HomePopular = entity.ShowOnPageAsPopular,

            });
        }
        [HttpPost] // request sonucunda bizden gen datayı alır ve db ye gönderir.
        public IActionResult UpdatePopular(ProductModel model)
        {
            var entity = _productService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();// bulunamadı 
            }
            model.HomePopular = entity.ShowOnPageAsPopular;
            _productService.Update(entity, null);
            return RedirectToAction("HomeMonth");
        }
    }
}
