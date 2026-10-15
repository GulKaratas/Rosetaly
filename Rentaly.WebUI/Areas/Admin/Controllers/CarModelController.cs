using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IBrandService _brandService;
        private readonly ICarService _carService;

        public CarModelController(ICarModelService carModelService, IBrandService brandService, ICarService carService)
        {
            _carModelService = carModelService;
            _brandService = brandService;
            _carService = carService;
        }

        public async Task<IActionResult> CarModelList()
        {
            var values = await _carModelService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCarModel()
        {
            await LoadBrands();
            return View(new CarModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarModel(CarModel carModel)
        {
            await _carModelService.TInsertAsync(carModel);
            return RedirectToAction("CarModelList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCarModel(int id)
        {
            var value = await _carModelService.TGetByIdAsync(id);
            await LoadBrands(value.BrandId);
            var cars = await _carService.TGetAllCarsWithCategoryAsync();
            ViewBag.Cars = cars.Where(x => x.CarModelId == id).ToList();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCarModel(CarModel carModel)
        {
            await _carModelService.TUpdateAsync(carModel);
            return RedirectToAction("CarModelList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            await _carModelService.TDeleteAsync(id);
            return RedirectToAction("CarModelList");
        }

        private async Task LoadBrands(int selectedBrandId = 0)
        {
            var brands = await _brandService.TGetListAsync();
            ViewBag.Brands = new SelectList(brands, "BrandId", "BrandName", selectedBrandId);
        }
    }
}
