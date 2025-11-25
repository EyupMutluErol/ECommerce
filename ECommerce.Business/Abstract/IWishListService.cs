using ECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface IWishListService
    {
        void DeleteFromWishlist(int cartid,int productId);
        Wishlist GetByUserId(string userId);
        Wishlist GetByUserWishlistId(string userId);

    }
}
