

using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Concrete;

public class EfCategoryRepository(AppDbContext _context) : EfGenericRepositoryBase<Category, AppDbContext>(_context), ICategoryRepository
{
    private readonly AppDbContext context = _context;
    public Category GetByIdWithProducts(int id)
    {
        return _context.Categories.Include(c=>c.ProductCategories).ThenInclude(pc=>pc.Product).SingleOrDefault(c=>c.Id==id);
    }
}
