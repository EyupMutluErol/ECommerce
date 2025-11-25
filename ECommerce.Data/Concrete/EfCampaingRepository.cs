
using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;

namespace ECommerce.Data.Concrete;

public class EfCampaingRepository : EfGenericRepositoryBase<Campaing, AppDbContext>, ICampaingRepository
{
    public EfCampaingRepository(AppDbContext _context) : base(_context)
    {
    }
}
