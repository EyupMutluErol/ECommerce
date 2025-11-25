

using ECommerce.Data.Abstract;
using ECommerce.Data.Context;
using ECommerce.Entities;
using ECommerce.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Concrete;

public class EfProductRepository(AppDbContext _context) : EfGenericRepositoryBase<Product, AppDbContext>(_context), IProductRepository
{
    private readonly AppDbContext _context = _context;


    public Product GetByIdWithCategories(int id)
    {

        return _context.Products.Include(X => X.ProductCategories).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id);

    }

    public List<Product> GetProductByCategory(string categoryName, int page, int pageSize)
    {

        var products = _context.Products.Include(x => x.ProductCategories).ThenInclude(x => x.Category).AsQueryable();
        if (!string.IsNullOrEmpty(categoryName))
        {
            products = products.Where(p => p.ProductCategories.Any(c => c.Category.Name.ToLower() == categoryName.ToLower()));
        }
        return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

    }

    public Product GetProductDetails(int productId)
    {
        return _context.Products.Include(x => x.ProductCategories).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == productId);


    }

    public int GetProductsByCategory(string categoryName)
    {
        var products = _context.Products.Include(x => x.ProductCategories).ThenInclude(x => x.Category).AsQueryable();
        if (!string.IsNullOrEmpty(categoryName))
        {
            products = products.Where(p => p.ProductCategories.Any(c => c.Category.Name.ToLower() == categoryName.ToLower()));
        }
        return products.Count();

    }



    public void Update(Product entity, int[] categoryIds)
    {

        var product = _context.Products.Include(x => x.ProductCategories).FirstOrDefault(x => x.Id == entity.Id);
        if (product != null)
        {
            product.Name = entity.Name;
            product.Brand = entity.Brand;
            product.Description = entity.Description;
            product.StockQuantity = entity.StockQuantity;
            product.ImageUrl = entity.ImageUrl;
            product.Price = entity.Price;
            product.DiscountedPrice = entity.DiscountedPrice;

            product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
            {
                CategoryId = catid,
                ProductId = entity.Id,
            }).ToList();

            _context.SaveChanges();

        }
    

    }

    public List<ProductStockDto> GetProductStockSummary()
    {
        return _context.Products.Where(p=>p.IsActive)
            .Select(p=> new ProductStockDto
            {
           
                ProductName = p.Name,
                StockQuantity = p.StockQuantity
            }).ToList();
    }
}
