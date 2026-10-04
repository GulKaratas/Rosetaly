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
    public class EfCarModelDal : GenericRepository<CarModel>, ICarModelDal
    {
        public EfCarModelDal(RentalyContext context) : base(context)
        {
        }
    }
}
