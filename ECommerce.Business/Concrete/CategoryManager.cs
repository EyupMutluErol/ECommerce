

using ECommerce.Business.Abstract;
using ECommerce.Data.Abstract;
using ECommerce.Entities;

namespace ECommerce.Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public bool Validate(Category entity)
    {
        var isValid = true;
        if (string.IsNullOrEmpty(entity.Name))
        {
            ErrorMessage = "The catetory name cannot be blank";
            isValid = false;
        }
        return isValid;
    }
    public string ErrorMessage { get; set; }

    public void Add(Category category)
    {
        if (Validate(category)) 
        { 
          _categoryRepository.Add(category);
           
        }
        
    }

    public void Delete(Category category)
    {
       _categoryRepository.Delete(category);
    }

    public List<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    public Category GetById(int id)
    {
      return _categoryRepository.GetById(id);
    }

    public Category GetByWithProduct(int id)
    {
       return _categoryRepository.GetByIdWithProducts(id);
    }

    public void Update(Category category)
    {
        _categoryRepository.Update(category);
    }

  
}
