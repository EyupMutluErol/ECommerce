

using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;

namespace ECommerce.Data.Concrete;

public class EfContactRepository : EfGenericRepositoryBase<Contact, AppDbContext>, IContactRepository
{
    public EfContactRepository(AppDbContext _context) : base(_context)
    {
    }
}
