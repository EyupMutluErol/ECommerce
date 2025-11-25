

using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;

namespace ECommerce.Data.Concrete;

public class EfCustomerCardRepository : EfGenericRepositoryBase<CustomerCard, AppDbContext>, ICustomerCardRepository
{
    public EfCustomerCardRepository(AppDbContext _context) : base(_context)
    {
    }
}
