using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface ICarService  
    {
        Task<List<Car>> TGetAllCarsWithCategoryAsync();
        Task<List<ResultCarDto>> TGetLatest10CarsAsync();
        Task<List<ResultCarDto>> TGetListAsync();
        Task<ResultCarDto> TGetByIdAsync(int id);
        Task TInsertAsync(CreateCarDto createCarDto);
        Task TUpdateAsync(UpdateCarDto updateCarDto);
        Task TDeleteAsync(int id);
        Task<int> TGetCountAsync();
    }
}
