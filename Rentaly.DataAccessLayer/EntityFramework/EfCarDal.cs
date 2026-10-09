using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfCarDal : GenericRepository<Car>, ICarDal
    {
        private readonly RentalyContext _context;

        public EfCarDal(RentalyContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Car>> GetAllCarsWithCategoryAsync()
        {
            return await _context.Cars
                .AsNoTracking()
                .Include(car => car.CarModel)
                    .ThenInclude(model => model.Brand)
                .Include(car => car.Category)
                .Include(car => car.Branch)
                .ToListAsync();
        }
        public async Task<List<Car>> GetLatest10CarsAsync()
        {
            return await _context.Cars
                .AsNoTracking()
                .Include(car => car.CarModel)
                    .ThenInclude(model => model.Brand)
                .Include(car => car.Category)
                .Where(car => car.IsActive)
                .OrderByDescending(car => car.CreatedAt)
                .ThenByDescending(car => car.CarId)
                .Take(10)
                .ToListAsync();
        }
    }
}
