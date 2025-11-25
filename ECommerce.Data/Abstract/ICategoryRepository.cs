

using ECommerce.Entities;

namespace ECommerce.Data.Abstract
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Category GetByIdWithProducts(int id);
    }
}
