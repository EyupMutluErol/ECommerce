using ECommerce.Entities;

namespace ECommerce.MvcUI.Models.CategoryModel
{
    public class CategoryListViewModel
    {
        public string  SelectedCategories { get; set; }
        public List<Category> Categories { get; set; }
    }
}
