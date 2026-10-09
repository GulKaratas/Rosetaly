using AutoMapper;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IMapper _mapper;

        public CarManager(ICarDal carDal, IMapper mapper)
        {
            _carDal = carDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _carDal.DeleteAsync(id);
        }

        public async Task<List<Car>> TGetAllCarsWithCategoryAsync()
        {
            return await _carDal.GetAllCarsWithCategoryAsync();
        }

        public async Task<List<ResultCarDto>> TGetLatest10CarsAsync()
        {
            var cars = await _carDal.GetLatest10CarsAsync();

            return cars.Select(car => new ResultCarDto
            {
                CarId = car.CarId,
                BrandName = car.CarModel.Brand.BrandName,
                ModelName = car.CarModel.ModelName,
                CategoryName = car.Category.CategoryName,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                SeatCount = car.SeatCount,
                LuggageCount = car.LuggageCount,
                FuelType = car.FuelType,
                Transmission = car.Transmission,
                PerformanceInfo = car.PerformanceInfo,
                DailyPrice = car.DailyPrice,
                Rating = car.Rating,
                IsAvailable = car.IsAvailable
            }).ToList();
        }
        public async Task<ResultCarDto> TGetByIdAsync(int id)
        {
            var car = await _carDal.GetByIdAsync(id);
            return _mapper.Map<ResultCarDto>(car);
        }

        public async Task<List<ResultCarDto>> TGetListAsync()
        {
           var cars = await _carDal.GetListAsync();
            return _mapper.Map<List<ResultCarDto>>(cars);
        }

        public async Task TInsertAsync(CreateCarDto createCarDto)
        {
            var car = _mapper.Map<Car>(createCarDto);
            await _carDal.InsertAsync(car);
        }

        public async Task TUpdateAsync(UpdateCarDto updateCarDto)
        {
            var car = _mapper.Map<Car>(updateCarDto);
            await _carDal.UpdateAsync(car);
        }

        public async Task<int> TGetCountAsync()
        {
            return await _carDal.CountAsync();
        }
    }
}

