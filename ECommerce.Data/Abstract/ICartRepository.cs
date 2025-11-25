

using ECommerce.Entities;

namespace ECommerce.Data.Abstract;

public interface ICartRepository:IGenericRepository<Cart>
{
    Cart GetByUserId(string userId);
    void DeleteFromCart(int cartId, int productId);
    void ClearCart(string cartId);

    
}
