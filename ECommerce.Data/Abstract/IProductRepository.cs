

using ECommerce.Entities;
using ECommerce.Entities.Dtos;

namespace ECommerce.Data.Abstract;

public interface IProductRepository:IGenericRepository<Product>
{
    List<Product> GetProductByCategory(string categoryName, int page,int pageSize);
    Product GetProductDetails(int productId);
    int GetProductsByCategory(string categoryName);
    Product GetByIdWithCategories(int id);
    void Update(Product entity, int[] categoryIds);
    List<ProductStockDto> GetProductStockSummary();



}


