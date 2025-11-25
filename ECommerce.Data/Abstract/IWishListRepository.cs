using ECommerce.Entities;

namespace ECommerce.Data.Abstract;

public interface IWishListRepository:IGenericRepository<Wishlist>
{
    Wishlist GetByUserId(string userId);
    void DeleteFromWishlist(int cartId,int productId);
    Wishlist GetByUserWhishlistId(string userId);
}
public interface IAsyncWishListRepository : IGenericRepository<Wishlist>
{
    Task DeleteFromWishlistAsync(int cartId, int productId);
    Task<Wishlist> GetByUserIdAsync(string userId);
    Task<Wishlist?> GetByUserWhishlistIdAsync(int userId);
}