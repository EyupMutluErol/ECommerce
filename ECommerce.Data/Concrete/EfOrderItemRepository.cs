
using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;

namespace ECommerce.Data.Concrete;

public class EfOrderItemRepository : EfGenericRepositoryBase<OrderItem, AppDbContext>, IOrderItemRepository
{
    public EfOrderItemRepository(AppDbContext _context) : base(_context)
    {
    }
}
