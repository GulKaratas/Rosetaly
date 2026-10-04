using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await _carDal.DeleteAsync(id);
        }

        public async Task<List<Car>> TGetAllCarsWithCategoryAsync()
        {
            return await _carDal.GetAllCarsWithCategoryAsync();
        }

        public async Task<Car> TGetByIdAsync(int id)
        {
            return await _carDal.GetByIdAsync(id);
        }

        public async Task<List<Car>> TGetListAsync()
        {
            return await _carDal.GetListAsync();
        }

        public async Task TInsertAsync(Car entity)
        {
            await _carDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Car entity)
        {
            await _carDal.UpdateAsync(entity);
        }
    }
}
