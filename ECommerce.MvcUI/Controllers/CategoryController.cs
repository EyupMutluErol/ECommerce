using ECommerce.Business.Abstract;
using ECommerce.MvcUI.Models.CategoryModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MvcUI.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(new CategoryListModel
            {
                Categories = _categoryService.GetAll().Where(x => x.Status == true).ToList()
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            _categoryService.Add(new ECommerce.Entities.Category
            {
                Name = model.Name,
                Status = true
            });
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Update(int id) 
        {
           var entity = _categoryService.GetById(id);
            if (entity ==null)
            {
                return NotFound();
            }
            return View( new CategoryModel
            {
                Id=entity.Id,
                Name=entity.Name
            });

        }
        [HttpPost]
        public IActionResult Update(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            _categoryService.Update(entity);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(CategoryModel categoryModel)
        {
            var entity = _categoryService.GetById(categoryModel.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Status = false;
            _categoryService.Update(entity);
            return RedirectToAction("Index");
        }

    }
}
