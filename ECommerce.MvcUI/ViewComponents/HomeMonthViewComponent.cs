using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Models.ProductModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.ViewComponents
{
    public class HomeMonthViewComponent:ViewComponent
    {
        private IProductService _productService;

        public HomeMonthViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        //Sadece ihtiyacımız olanı cekiyoruz 

        public IViewComponentResult Invoke()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll().Where(x => x.ShowOnPageAsMonthlyHighlight == true).Take(6).ToList()
            });

        }


    }
}
