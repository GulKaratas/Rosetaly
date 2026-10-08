using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.CategoryDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryList()
        {
            var categories = await _categoryService.TGetListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto category)
        {
            
                await _categoryService.TInsertAsync(category);
                return RedirectToAction("CategoryList");
          
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.TDeleteAsync(id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryService.TGetByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto category)
        {
            await _categoryService.TUpdateAsync(category);
            return RedirectToAction("CategoryList");
        }
    }
}
