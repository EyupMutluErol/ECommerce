

using ECommerce.Business.Abstract;
using ECommerce.Data.Abstract;
using ECommerce.Entities;

namespace ECommerce.Business.Concrete;

public class CartManager : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IWishListRepository _wishListRepository;

    public CartManager(ICartRepository cartRepository, IWishListRepository wishListRepository)
    {
        _cartRepository = cartRepository;
        _wishListRepository = wishListRepository;
    }

    public void AddToCart(string userId, int productId, int quantity)
    {
        var cart= GetCartByUserId(userId);
        if (cart == null)
        {
            cart=InitializeCart(userId);
            _cartRepository.Add(cart);
        }
        if (cart.CartItems ==null)
        {
            cart.CartItems = new List<CartItem>();
        }
        var existingCartItem= cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
        if (existingCartItem == null)
        {
            cart.CartItems.Add(new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                CartId = cart.Id
            });
        }
        else
        {
            existingCartItem.Quantity += quantity;
            
        }

        _cartRepository.Update(cart);



    }

    public void AddToWishlist(string userId, int productId)
    {
       var cart = GetCartByUserWishlistId(userId);
        if (cart != null) 
        {
        var index= cart.Wishlists.FindIndex(i => i.ProductId == productId);
            if (index<0)
            {
                cart.Wishlists.Add(new Wishlist
                {
                    ProductId = productId,
                    CartId = cart.Id
                });
            }
            else
            {
                /*ihtiyac durumunda yazılacaktır. */
            }
            _cartRepository.Update(cart);
        
        }
    }

    public void ClearCart(string cartId)
    {
        _cartRepository.ClearCart(cartId);
    }

    public void DeleteFromCart(string userId, int productId)
    {
      var cart =GetCartByUserId(userId);
        if(cart != null)
        {
            var cartId=cart.Id;
            _cartRepository.DeleteFromCart(cartId,productId);
        }
    }

    public void DeleteFromWishlist(string userId, int productId)
    {
        var cart = GetCartByUserWishlistId(userId);
        if (cart != null) {

            var cartId = cart.Id;
           _wishListRepository.DeleteFromWishlist(cartId,productId);
        
        }
    }

    public Cart GetCartByUserId(string userId)
    {
        return _cartRepository.GetByUserId(userId);
    }
    //todo: burası daha sonrasında wishlist yapısına entegre edilecektir. 
    public Cart GetCartByUserWishlistId(string userId)
    {
        return _cartRepository.GetByUserId(userId);
    }
    //AddSingleton,AddScoped,AddTransient Nedir?
    public Cart InitializeCart(string userId)
    {
        var cart = new Cart
        {
            UserId = userId,
            CartItems= new List<CartItem>(),
        };
        _cartRepository.Add(cart);
        return cart;
    }
}
