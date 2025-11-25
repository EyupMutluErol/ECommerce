using ECommerce.Business.Abstract;
using ECommerce.Data.Abstract;
using ECommerce.Entities;

namespace ECommerce.Business.Concrete;

public class WishListManager : IWishListService
{
    private readonly IWishListRepository _wishListRepository;

    public WishListManager(IWishListRepository wishListRepository)
    {
        _wishListRepository = wishListRepository;
    }

    public void DeleteFromWishlist(int cartid, int productId)
    {
       _wishListRepository.DeleteFromWishlist(cartid, productId);
    }

    public Wishlist GetByUserId(string userId)
    {
       return _wishListRepository.GetByUserId(userId);
    }

    public Wishlist GetByUserWishlistId(string userId)
    {
        return _wishListRepository.GetByUserWhishlistId(userId);
    }
}
