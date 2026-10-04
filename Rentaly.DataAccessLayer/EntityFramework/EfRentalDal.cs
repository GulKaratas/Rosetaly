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
    public class EfRentalDal : GenericRepository<Rental>, IRentalDal
    {
        public EfRentalDal(RentalyContext context) : base(context)
        {
        }
    }
}
