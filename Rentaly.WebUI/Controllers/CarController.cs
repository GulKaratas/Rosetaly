using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.WebUI.Models;

namespace Rentaly.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;

        public CarController(
            ICarService carService,
            IBrandService brandService,
            ICategoryService categoryService,
            IRentalService rentalService,
            IMapper mapper)
        {
            _carService = carService;
            _brandService = brandService;
            _categoryService = categoryService;
            _rentalService = rentalService;
            _mapper = mapper;
        }

        public async Task<IActionResult> CarList(DateTime? pickup, DateTime? returnAt, int? categoryId, int? branchId)
        {
            var carValues = await _carService.TGetAllCarsWithCategoryAsync();
            carValues = carValues.Where(car => car.IsAvailable && car.IsActive).ToList();

            if (categoryId is > 0)
            {
                carValues = carValues.Where(car => car.CategoryId == categoryId).ToList();
            }

            if (pickup.HasValue && returnAt.HasValue && returnAt > pickup)
            {
                var busyCarIds = await _rentalService.TGetApprovedOverlapCarIdsAsync(pickup.Value, returnAt.Value);
                carValues = carValues.Where(car => !busyCarIds.Contains(car.CarId)).ToList();
            }

            var cars = _mapper.Map<List<ResultCarDto>>(carValues);

            var brands = await _brandService.TGetListAsync();
            var categories = await _categoryService.TGetListAsync();

            var model = new CarListViewModel
            {
                Cars = cars,
                Brands = DistinctNames(brands.Select(x => x.BrandName)),
                Categories = DistinctNames(categories.Select(x => x.CategoryName)),
                SeatCounts = cars
                    .Select(x => x.SeatCount)
                    .Where(x => x > 0)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList(),
                FuelTypes = DistinctNames(cars.Select(x => x.FuelType)),
                Pickup = pickup,
                ReturnDate = returnAt
            };

            return View(model);
        }

        private static List<string> DistinctNames(IEnumerable<string?> values)
        {
            return values
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x!.Trim())
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .OrderBy(x => x)
                .ToList();
        }
        [HttpGet]
        
        public async Task<IActionResult> CarsDetails(int id)
        {
            var carValues = await _carService.TGetAllCarsWithCategoryAsync();
            var carValue = carValues.FirstOrDefault(x => x.CarId == id);

            

            var car = _mapper.Map<GetCarByIdDto>(carValue);
            return View(car);
        }
    }
}
