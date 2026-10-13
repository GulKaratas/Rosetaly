using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rendalService)
        {
            _rentalService = rendalService;
        }

        public async Task<IActionResult> RentalList()
        {
            var values = await _rentalService.TGetListAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateRental()
        {
            var values = await _rentalService.TGetListAsync();
            return View(values);
        }
    }
}
