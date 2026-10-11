using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.About;
using Rentaly.WebUI.Models;

namespace Rentaly.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly IAboutService _aboutService;
        private readonly ICarService _carService;
        private readonly IRentalService _rentalService;
        private readonly IReviewService _reviewService;

        public _DefaultAboutComponentPartial(
            IAboutService aboutService,
            ICarService carService,
            IRentalService rentalService,
            IReviewService reviewService)
        {
            _aboutService = aboutService;
            _carService = carService;
            _rentalService = rentalService;
            _reviewService = reviewService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var aboutList = await _aboutService.TGetListAsync();
            var aboutDto = aboutList.FirstOrDefault();
           

            var totalCars = await _carService.TGetCountAsync(); 
            var totalRentals = await _rentalService.TGetCountAsync();
            var distinctCustomers = await _reviewService.TGetDistinctCustomerCountAsync();

            var viewModel = new AboutComponentViewModel
            {
                About = aboutDto, 
                TotalCars = totalCars,
                TotalRentals = totalRentals,
                TotalReviews = distinctCustomers,
                ExperienceYears = DateTime.Now.Year - 2011
            };

            return View(viewModel);
        }
    }
}
