

namespace ECommerce.Entities;

public class Category
{
    public int Id { get; set; }
    public string  Name { get; set; }
    public bool  Status { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }

}
