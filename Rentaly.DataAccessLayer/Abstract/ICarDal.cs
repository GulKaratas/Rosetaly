using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.Abstract
{
    public interface ICarDal : IGenericDal<Car>
    {
        Task<List<Car>> GetAllCarsWithCategoryAsync();
    }
}
