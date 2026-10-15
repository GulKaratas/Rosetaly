using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfCarModelDal : GenericRepository<CarModel>, ICarModelDal
    {
        private readonly RentalyContext _context;

        public EfCarModelDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<CarModel>> GetListAsync()
        {
            return await _context.CarModels.Include(x => x.Brand).ToListAsync();
        }

        public override async Task<CarModel> GetByIdAsync(int id)
        {
            return await _context.CarModels.Include(x => x.Brand).FirstOrDefaultAsync(x => x.CarModelId == id);
        }
    }
}
