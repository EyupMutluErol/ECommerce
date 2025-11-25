

using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;

namespace ECommerce.Data.Concrete
{
    public class EfCustomerAddressRepository : EfGenericRepositoryBase<CustomerAddress, AppDbContext>, ICustomerAddressRepository
    {
        public EfCustomerAddressRepository(AppDbContext _context) : base(_context)
        {
        }
    }
}
