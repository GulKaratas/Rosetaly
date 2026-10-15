using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.DataAccessLayer.EntityFramework
{
    public class EfRentalDal : GenericRepository<Rental>, IRentalDal
    {
        private readonly RentalyContext _context;

        public EfRentalDal(RentalyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<int>> GetApprovedOverlapCarIdsAsync(DateTime pickup, DateTime returnDate)
        {
            return await _context.Rentals.AsNoTracking()
                .Where(rental =>
                    rental.Status == "Onaylı"
                    && rental.PickupDate < returnDate
                    && rental.ReturnDate > pickup)
                .Select(rental => rental.CarId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> HasApprovedOverlapAsync(int carId, DateTime pickup, DateTime returnDate, int excludeRentalId)
        {
            return await _context.Rentals.AsNoTracking().AnyAsync(rental =>
                rental.CarId == carId
                && rental.RentalId != excludeRentalId
                && rental.Status == "Onaylı"
                && rental.PickupDate < returnDate
                && rental.ReturnDate > pickup);
        }

        public override async Task UpdateAsync(Rental entity)
        {
            var incoming = _context.Entry(entity);
            var keyValues = incoming.Metadata.FindPrimaryKey()!
                .Properties
                .Select(property => incoming.Property(property.Name).CurrentValue)
                .ToArray();

            var tracked = await _context.Rentals.FindAsync(keyValues);
            if (tracked != null && !ReferenceEquals(tracked, entity))
            {
                _context.Entry(tracked).CurrentValues.SetValues(entity);
            }
            else if (tracked == null)
            {
                _context.Rentals.Update(entity);
            }

            await _context.SaveChangesAsync();
        }
    }
}
