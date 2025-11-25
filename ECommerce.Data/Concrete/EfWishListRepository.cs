
using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Concrete;

public class EfWishListRepository(AppDbContext _context) : EfGenericRepositoryBase<Wishlist, AppDbContext>(_context), IWishListRepository
{
   private readonly AppDbContext context = _context;

    public void DeleteFromWishlist(int cartId,int productId)
    {
        //1 yontem

        //using (var context=new AppDbContext())
        //{
        //    //var cmd = @"delete from Wishlist where CartId=@p0 and ProductId=@p2";
        //    var cmd = @"delete from Wishlist where CardId ={0} and ProductId={1}";
        //    context.Database.ExecuteSqlRaw(cmd,cartId, productId);
        //}

        //2 yontem
        //using (var context = new AppContext())
        //{
        //    var item =  context.Wishlists.FirstOrDefault(w => w.CartId == cartId && w.ProductId == productId);
        //    if (item != null)
        //    {
        //        context.Wishlists.Remove(item);
        //         context.SaveChangesAsync();
        //    }

        //}

        
        //3 yontem

        var item = context.Wishlists.FirstOrDefault(w => w.CartId == cartId && w.ProductId == productId);
        if(item != null)
        {
            context.Wishlists.Remove(item);
            context.SaveChanges();
        }

    }

    public Wishlist GetByUserId(string userId)
    {

    
            var cmd = @"Select Top 1 * from Wishlist w Inner Join Carts c on w.CartId = c.Id where c.UserId={0}";
            return context.Wishlists.FromSqlRaw(cmd, userId).Include(w=>w.Product).FirstOrDefault();

    }

    public Wishlist GetByUserWhishlistId(string userId)
    {
        
          return GetByUserId(userId);


    }
}


//Garbage Collector calısma mantıgını => sonrasında asenkron programlama ardından 
public class EfWishListAsyncRepository : EfGenericRepositoryBase<Wishlist, AppDbContext>, IAsyncWishListRepository
{
    public EfWishListAsyncRepository(AppDbContext _context) : base(_context)
    {
    }

    public async Task DeleteFromWishlistAsync(int cartId, int productId)
    {
        using (var context = new AppDbContext())
        {
            var item=await context.Wishlists.FirstOrDefaultAsync(w=>w.CartId==cartId&&w.ProductId==productId);
            if (item != null)
            {
                context.Wishlists.Remove(item);
                await context.SaveChangesAsync();
            }

        }
    }

    public async Task<Wishlist> GetByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Wishlist?> GetByUserWhishlistIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}