using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.ViewComponents
{
    public class CategoryListViewComponent: ViewComponent
    {
        private ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new CategoryListViewModel
            {
                SelectedCategory = RouteData.Values["category"]?.ToString(),
                Categories = _categoryService.GetAll().Where(x => x.Status == true).ToList()
            });
        }

    }
}
