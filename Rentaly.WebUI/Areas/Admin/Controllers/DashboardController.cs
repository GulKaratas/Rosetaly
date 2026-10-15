using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.WebUI.Models;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;
        private readonly IBranchService _branchService;
        private readonly IRentalService _rentalService;
        private readonly IAiOperationsService _aiOperationsService;

        public DashboardController(
            ICarService carService,
            ICustomerService customerService,
            IBranchService branchService,
            IRentalService rentalService,
            IAiOperationsService aiOperationsService)
        {
            _carService = carService;
            _branchService = branchService;
            _customerService = customerService;
            _rentalService = rentalService;
            _aiOperationsService = aiOperationsService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.TGetLatest10CarsAsync();
            var customers = await _customerService.TGetListAsync();
            var branches = await _branchService.TGetListAsync();
            var rentals = await _rentalService.TGetListAsync();

            var today = DateTime.Today;

            var model = new DashboardViewModel
            {
                CarCount = cars.Count,
                AvailableCarCount = cars.Count(x => x.IsAvailable),
                CustomerCount = customers.Count,
                BranchCount = branches.Count,
                PendingRentalCount = rentals.Count(x => x.Status == "Bekliyor"),
                ApprovedRentalCount = rentals.Count(x => x.Status == "Onaylı"),
                ApprovedRevenue = rentals.Where(x => x.Status == "Onaylı").Sum(x => x.TotalPrice),
                RecentRentals = rentals.OrderByDescending(x => x.RentalId).Take(8).ToList(),
                RecentCars = cars.Take(5).ToList(),
                TodayPickups = rentals.Where(x => x.PickupDate.Date == today).ToList(),
                TodayReturns = rentals.Where(x => x.ReturnDate.Date == today).ToList(),
                MorningBriefing = await _aiOperationsService.BuildMorningBriefingAsync(),
                PendingAnalyses = await _aiOperationsService.BuildPendingAnalysesAsync()
            };

            return View(model);
        }
    }
}
