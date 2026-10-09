
using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultTopCarsComponentPartial : ViewComponent
    {
        private readonly ICarService _carService;

        public _DefaultTopCarsComponentPartial(
            ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cars = await _carService.TGetLatest10CarsAsync();

            return View(cars);
        }
    }
}