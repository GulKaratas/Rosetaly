using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.ValidationRules;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;

        private readonly IBranchService _branchService;
        private readonly IBrandService _brandService;
        private readonly ICarModelService _carModelService;
        private readonly IValidator<CreateCarDto> _carValidator;

        public CarController(ICarService carService, ICategoryService categoryService, IBranchService branchService, IBrandService brandService, ICarModelService carModelService, IValidator<CreateCarDto> carValidator)
        {
            _carService = carService;
            _categoryService = categoryService;
            _branchService = branchService;
            _brandService = brandService;
            _carModelService = carModelService;
            _carValidator = carValidator;
        }
        public async Task<IActionResult> CarList()
        {
            var values = await _carService.TGetAllCarsWithCategoryAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            ViewBag.Categories = new SelectList(await _categoryService.TGetListAsync(), "CategoryId", "CategoryName"); 
            ViewBag.Brands = await _brandService.TGetListAsync();
            ViewBag.CarModels = await _carModelService.TGetListAsync();
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName"); 
            return View();
            
        }


        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto car)
        {
            var validation = await _carValidator.ValidateAsync(car);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                ViewBag.Categories = new SelectList(await _categoryService.TGetListAsync(), "CategoryId", "CategoryName", car.CategoryId);
                ViewBag.Brands = await _brandService.TGetListAsync();
                ViewBag.CarModels = await _carModelService.TGetListAsync();
                ViewBag.SelectedBrandId = Request.Form["BrandId"].ToString();
                ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName", car.BranchId);
                return View(car);
            }

            await _carService.TInsertAsync(car);
            return RedirectToAction("CarList");
        }
    }
}
