

namespace ECommerce.Entities;

public class Product
{
    public int Id { get; set; }
    public string  Name { get; set; }
    public string Brand { get; set; }
    public string  ImageUrl { get; set; }
    public string  Description { get; set; }

    public short  StockQuantity { get; set; }
    public decimal? Price { get; set; }
    public decimal? DiscountedPrice { get; set; }

    public bool ShowOnPageAsMonthlyHighlight { get; set; }
    public bool ShowOnPageAsDailyHighlight { get; set; }
    public bool ShowOnPageAsSpecialOffer { get; set; }
    public bool ShowOnPageAsPopular { get; set; }

    public bool  IsActive { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }



}
