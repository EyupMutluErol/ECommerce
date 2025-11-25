using ECommerce.Entities;

namespace ECommerce.MvcUI.Models
{
    public class CategoryListViewModel
    {
        public string  SelectedCategory { get; set; }
        public List<Category> Categories { get; set; }
    }
}
