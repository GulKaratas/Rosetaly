using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface ICarService : IGenericService<Car>
    {
        Task<List<Car>> TGetAllCarsWithCategoryAsync();
    }
}
