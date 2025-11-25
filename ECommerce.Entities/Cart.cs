

namespace ECommerce.Entities;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public List<CartItem> CartItems { get; set; }
    public List<Wishlist> Wishlists { get; set; }

}
