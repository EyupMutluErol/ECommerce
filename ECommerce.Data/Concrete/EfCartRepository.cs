using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Concrete
{
    public class EfCartRepository(AppDbContext _context) : EfGenericRepositoryBase<Cart, AppDbContext>(_context), ICartRepository
    {
        private readonly AppDbContext context = _context;

        public void ClearCart(string cartId)
        {


            var cmd = @"delete from CartItem where CartId=@p0";
            _context.Database.ExecuteSqlRaw(cmd, cartId);

        }

        public void DeleteFromCart(int cartId, int productId)
        {
            //1 yöntem

            var cmd = @"delete from CartItem where CartId=@p0 and ProductId=@p1";
            _context.Database.ExecuteSqlRaw(cmd, cartId, productId);

            //2 yöntem  CartItems nesnesini AppDbContext içine eklmemiz gerekmektedir. ve aynı zamanda migrations yapmamız gerekir. 
            //var entity = _context.CartItems.FirstOrDefault(i => i.CartId == cartId && i.ProductId == productId);
            //if(entity != null)
            //{
            //    _context.cartItems.Remove(entity);
            //    _context.SaveChanges();
            //}

        }

        public Cart GetByUserId(string userId)
        {
            var x = _context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Product).FirstOrDefault(i => i.UserId == userId);

            return _context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Product).FirstOrDefault(i => i.UserId == userId);

        }
    }
}
