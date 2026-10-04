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
        public EfCarDal(RentalyContext context) : base(context)
        {
        }

        public async Task<List<Car>> GetAllCarsWithCategoryAsync()
        {
            var context = new RentalyContext();
            var values = await context.Cars.Include(c => c.Category).ToListAsync();
            return values;
        }
    }
}
