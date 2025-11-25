

using ECommerce.Entities;
using ECommerce.Entities.Dtos;

namespace ECommerce.Business.Abstract;

public interface IProductService:IValidator<Product>
{

    Product GetById(int id);
    Product GetProductDetails(int id);
    List<Product>GetAll();
    List<Product>GetProductsByCategory(string category,int page,int pageSize);
    bool Add(Product product);
    void Update(Product product);
    void Delete(Product product);
    int GetCountByCategory(string category);
    Product GetByIdWithCategories(int id);
    void Update(Product product, int[] categoryIds);
    List<ProductStockDto> GetProductStockSummary();

}
