
using ECommerce.Entities;

namespace ECommerce.Business.Abstract;

public interface ICategoryService:IValidator<Category>
{
    Category GetById(int id);
    Category GetByWithProduct(int id);
    List<Category> GetAll();
    void Add(Category category);
    void Update(Category category);
    void Delete(Category category);
}
