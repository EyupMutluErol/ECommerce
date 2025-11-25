using ECommerce.Entities;

namespace ECommerce.MvcUI.Models.CategoryModel
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public List<Product> Products { get; set; }
    }
}
