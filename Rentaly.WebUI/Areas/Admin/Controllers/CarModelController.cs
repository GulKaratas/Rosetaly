using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;

        public CarModelController(ICarModelService carModelService)
        {
            _carModelService = carModelService;
        }

        public async Task<IActionResult> CarModelList()
        {
            var values = await _carModelService.TGetListAsync();
            return View(values);

        }

        [HttpGet]
        public async Task<IActionResult> CreateCarModel()
        {
            var values = await _carModelService.TGetListAsync();
            return View();
        }


    }
}
