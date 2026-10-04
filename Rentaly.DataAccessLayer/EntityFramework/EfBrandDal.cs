using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;


namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfBrandDal : GenericRepository<Brand>, IBrandDal
    {
        public EfBrandDal(RentalyContext context) : base(context)
        {
        }
    }
}
