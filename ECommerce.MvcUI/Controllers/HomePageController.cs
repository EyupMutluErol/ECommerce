using ECommerce.Business.Abstract;
using ECommerce.Entities;
using ECommerce.MvcUI.Models.ProductModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{
    public class HomePageController : Controller
    {

        private IProductService _productService;

        public HomePageController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll()
            });
        }
        public PartialViewResult Slider()
        {
            return PartialView();
        }

        public PartialViewResult HomeCategory()
        {
            return PartialView();
        }
        public IActionResult ProductList(string category, int page = 1)
        {
            const int pageSize = 9;
            return View(new ProductListModel
            {
                PageInfo = new PageInfo
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemPerPage = pageSize,
                    CurrentCategory = category
                },
                Products = _productService.GetProductsByCategory(category, page, pageSize)
            });
        }

        public PartialViewResult PartialProductList()
        {
            return PartialView();
        }
        public IActionResult ProductDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails((int)id);
            if (product == null)
            {
                return NotFound();
            }
           return View(new ProductDetailsModel
           {
               Product = product,
               Categories=product.ProductCategories.Select(i => i.Category).ToList()
           });
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult SearchProduct(string q)
        {
            var word = HttpContext.Request.Query["q"].ToString();
            return View(new ProductListModel
            {
                Products = _productService.GetAll().Where(i => i.Name.ToLower().Contains(word.ToLower()) == word.ToLower().Contains(word.ToLower())).ToList()
            });
            
        }



    }
}

/*
 
 View component : aspnet core yeniden kullabilir UI parçaları olusturmak icin kullanılan yapıdır. 

---Sepet
---Yorum 
---Haftanın ilanları 
---Ayın İlanları 
 
 */