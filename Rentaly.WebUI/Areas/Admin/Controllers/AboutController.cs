using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> AboutList()
        {
            var values = await _aboutService.TGetListAsync();
            return View(values);

        }

        [HttpGet]
        public async Task<IActionResult> CreateAbout()
        {
            var values = await _aboutService.TGetListAsync();
            return View();
        }


    }
}
