using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Models.ProductModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.ViewComponents
{
    public class HomeOfferViewComponent: ViewComponent
    {

        private IProductService _productService;

        public HomeOfferViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        //Sadece ihtiyacımız olanı cekiyoruz 

        public IViewComponentResult Invoke()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll().Where(x => x.ShowOnPageAsPopular == true).Take(6).ToList()
            });

        }
    }
}
