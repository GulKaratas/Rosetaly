using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;


namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfBrandDal : GenericRepository<Brand>, IBrandDal
    {
        private readonly RentalyContext _context;

        public EfBrandDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Brand>> GetListAsync()
        {
            return await _context.Brands.Include(x => x.CarModels).ToListAsync();
        }

        public override async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.Brands.Include(x => x.CarModels).FirstOrDefaultAsync(x => x.BrandId == id);
        }
    }
}
