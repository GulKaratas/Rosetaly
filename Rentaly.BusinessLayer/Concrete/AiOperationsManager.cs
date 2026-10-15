using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DtoLayer.RentalDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class AiOperationsManager : IAiOperationsService
    {
        private readonly IRentalDal _rentalDal;
        private readonly ICarDal _carDal;

        public AiOperationsManager(IRentalDal rentalDal, ICarDal carDal)
        {
            _rentalDal = rentalDal;
            _carDal = carDal;
        }

        public async Task<string> BuildMorningBriefingAsync()
        {
            var rentals = await _rentalDal.GetListAsync();
            var today = DateTime.Today;
            var pickups = rentals.Count(x => x.Status == "Onaylı" && x.PickupDate.Date == today);
            var returns = rentals.Count(x => x.Status == "Onaylı" && x.ReturnDate.Date == today);
            var pending = rentals.Count(x => x.Status == "Bekliyor");

            return $"Bugün {pickups} onaylı alış, {returns} onaylı iade var. Onay bekleyen {pending} talep bulunuyor.";
        }

        public async Task<List<PendingRentalAnalysisDto>> BuildPendingAnalysesAsync()
        {
            var rentals = await _rentalDal.GetListAsync();
            var cars = await _carDal.GetAllCarsWithCategoryAsync();
            var pending = rentals
                .Where(x => x.Status == "Bekliyor")
                .OrderBy(x => x.PickupDate)
                .Take(4)
                .ToList();

            return pending.Select(rental =>
            {
                var car = cars.FirstOrDefault(x => x.CarId == rental.CarId);
                var overlaps = rentals.Where(x =>
                    x.RentalId != rental.RentalId
                    && x.CarId == rental.CarId
                    && (x.Status == "Onaylı" || x.Status == "Bekliyor")
                    && x.PickupDate < rental.ReturnDate
                    && x.ReturnDate > rental.PickupDate).ToList();
                var conflict = overlaps.FirstOrDefault(x => x.Status == "Onaylı")
                    ?? overlaps.FirstOrDefault();

                var busyCarIds = rentals
                    .Where(x =>
                        x.RentalId != rental.RentalId
                        && (x.Status == "Onaylı" || x.Status == "Bekliyor")
                        && x.PickupDate < rental.ReturnDate
                        && x.ReturnDate > rental.PickupDate)
                    .Select(x => x.CarId)
                    .ToHashSet();

                var alternative = conflict == null
                    ? null
                    : cars.FirstOrDefault(x =>
                        x.CarId != rental.CarId
                        && x.IsActive
                        && x.IsAvailable
                        && !busyCarIds.Contains(x.CarId));

                return new PendingRentalAnalysisDto
                {
                    RentalId = rental.RentalId,
                    CarName = CarName(car),
                    PlateNumber = car?.PlateNumber ?? "",
                    PickupDate = rental.PickupDate,
                    ReturnDate = rental.ReturnDate,
                    HasConflict = conflict != null,
                    ConflictingRentalId = conflict?.RentalId,
                    ConflictingStatus = conflict?.Status ?? "",
                    AlternativePlate = alternative?.PlateNumber ?? ""
                };
            })
            .OrderByDescending(x => x.HasConflict)
            .ThenBy(x => x.PickupDate)
            .ToList();
        }

        private static string CarName(Car? car)
        {
            if (car?.CarModel == null)
            {
                return "Araç";
            }

            var brand = car.CarModel.Brand?.BrandName;
            var model = car.CarModel.ModelName;
            var name = $"{brand} {model}".Trim();
            return string.IsNullOrWhiteSpace(name) ? "Araç" : name;
        }
    }
}
