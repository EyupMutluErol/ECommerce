using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{
    public class StatisticController : Controller
    {
        private IOrderService _orderService;
        private IOrderItemService _orderItemService;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public StatisticController(IOrderService orderService, IOrderItemService orderItemService, IProductService productService, ICategoryService categoryService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _roleManager = roleManager;
        }




        public IActionResult Index()
        {

            //OrderStatic

            var values1 =_orderService.GetAll().Count().ToString();
            ViewBag.v1 = values1;
            var values2 = _orderService.GetAll().GroupBy(x => x.FirstName).OrderByDescending(y => y.Count()).Select(z => z.Key).FirstOrDefault();
            ViewBag.v2 = values2;
            var values3 = _orderService.GetAll().GroupBy(x => x.City).OrderByDescending(y => y.Count()).Select(z=>z.Key).FirstOrDefault();
            ViewBag.v3 = values3;
            var values4 = _orderItemService.GetAll().Sum(x => x.Price).ToString();
            ViewBag.v4 = values4;

            //ProductStatic
            var values5 = _productService.GetAll().Count().ToString();
            ViewBag.v5 = values5;
            var values6 =_productService.GetAll().OrderByDescending(x => x.Price).Select(y => y.Name).FirstOrDefault();
            ViewBag.v6 = values6;
            var values7 = _productService.GetAll().OrderBy(x => x.Price).Select(y => y.Name).FirstOrDefault();
            ViewBag.v7 = values7;
            var values8 = _productService.GetAll().Select(x=>x.Brand).Distinct().Count().ToString();
            ViewBag.v8 = values8;


            //Stock Static
            var values9 = _productService.GetAll().Sum(x => x.StockQuantity).ToString();
            ViewBag.v9 = values9;
            var values10 = _productService.GetAll().OrderBy(x=>x.StockQuantity).Select(y=>y.Name).FirstOrDefault();
            ViewBag.v10 = values10;
            var values11 = _productService.GetAll().OrderByDescending(x=>x.StockQuantity).Select(y=>y.Name).FirstOrDefault();
            ViewBag.v11 = values11;
            var values12 = _productService.GetAll().Count(x=>x.StockQuantity<50).ToString();
            ViewBag.v12 = values12;

            var values13 = _categoryService.GetAll().Count().ToString();
            ViewBag.v13 = values13;
            var values14 = _productService.GetAll().GroupBy(x=>x.Brand).OrderByDescending(y=>y.Count()).Select(z=>z.Key).FirstOrDefault();
            ViewBag.v14 = values14;
            var values15=_roleManager.Roles.Count().ToString();
            ViewBag.v15 = values15;
            var values16 = _userManager.Users.Count().ToString();
            ViewBag.v16 = values16;

            return View();
        }
    }
}


//15 dk ara
