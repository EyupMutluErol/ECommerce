using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Models.ProductModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.ViewComponents
{
    public class HomeGalleryViewComponent:ViewComponent
    {
        private readonly IProductService _productService;

        public HomeGalleryViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll().Where(x => x.ShowOnPageAsPopular == true).Take(8).ToList()
            });
        }

    }
}
