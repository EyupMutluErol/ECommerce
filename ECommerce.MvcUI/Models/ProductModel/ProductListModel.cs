using ECommerce.Entities;

namespace ECommerce.MvcUI.Models.ProductModel
{
    public class PageInfo
    {

        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemPerPage);

    }
    public class ProductListModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Product> Products { get; set; }
    }
}
