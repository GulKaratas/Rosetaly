using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CarDtos;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;

        private readonly IBranchService _branchService;
        private readonly ICarModelService _carModelService;
        private readonly IValidator<CreateCarDto> _carValidator;

        public CarController(ICarService carService, ICategoryService categoryService, IBranchService branchService, ICarModelService carModelService, IValidator<CreateCarDto> carValidator)
        {
            _carService = carService;
            _categoryService = categoryService;
            _branchService = branchService;
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
            await LoadLookups();
            return View(new CreateCarDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto car)
        {
            var validation = await _carValidator.ValidateAsync(car);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                await LoadLookups(car.CategoryId, car.BranchId, car.CarModelId);
                return View(car);
            }

            await _carService.TInsertAsync(car);
            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var car = await _carService.TGetByIdAsync(id);
            await LoadLookups(car.CategoryId, car.BranchId, car.CarModelId);
            ViewBag.Summary = car;
            return View(ToUpdateDto(car));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarDto car)
        {
            var check = new CreateCarDto
            {
                PlateNumber = car.PlateNumber,
                VIN = car.VIN,
                CarModelId = car.CarModelId,
                CategoryId = car.CategoryId,
                BranchId = car.BranchId,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                Kilometer = car.Kilometer,
                SeatCount = car.SeatCount,
                LuggageCount = car.LuggageCount,
                FuelType = car.FuelType,
                Transmission = car.Transmission,
                PerformanceInfo = car.PerformanceInfo,
                DailyPrice = car.DailyPrice,
                DepositAmount = car.DepositAmount
            };

            var validation = await _carValidator.ValidateAsync(check);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                await LoadLookups(car.CategoryId, car.BranchId, car.CarModelId);
                ViewBag.Summary = await _carService.TGetByIdAsync(car.CarId);
                return View(car);
            }

            await _carService.TUpdateAsync(car);
            return RedirectToAction("CarList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.TDeleteAsync(id);
            return RedirectToAction("CarList");
        }

        private async Task LoadLookups(int categoryId = 0, int branchId = 0, int carModelId = 0)
        {
            ViewBag.Categories = new SelectList(await _categoryService.TGetListAsync(), "CategoryId", "CategoryName", categoryId);
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName", branchId);
            var models = await _carModelService.TGetListAsync();
            var modelOptions = models.Select(m => new
            {
                m.CarModelId,
                Name = m.Brand.BrandName + " " + m.ModelName
            });
            ViewBag.CarModels = new SelectList(modelOptions, "CarModelId", "Name", carModelId);
        }

        private static UpdateCarDto ToUpdateDto(GetCarByIdDto car)
        {
            return new UpdateCarDto
            {
                CarId = car.CarId,
                PlateNumber = car.PlateNumber,
                VIN = car.VIN,
                CarModelId = car.CarModelId,
                CategoryId = car.CategoryId,
                BranchId = car.BranchId,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                Kilometer = car.Kilometer,
                SeatCount = car.SeatCount,
                LuggageCount = car.LuggageCount,
                FuelType = car.FuelType,
                Transmission = car.Transmission,
                PerformanceInfo = car.PerformanceInfo,
                DailyPrice = car.DailyPrice,
                DepositAmount = car.DepositAmount,
                IsAvailable = car.IsAvailable,
                IsActive = car.IsActive
            };
        }
    }
}
