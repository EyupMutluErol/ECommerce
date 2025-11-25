using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Models.ProductModel;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.ViewComponents
{
    public class HomeTodayViewComponent:ViewComponent
    {

        private IProductService _productService;

        public HomeTodayViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll().Where(x => x.ShowOnPageAsDailyHighlight == true).Take(6).ToList()

            });

        }
      


    }
}
