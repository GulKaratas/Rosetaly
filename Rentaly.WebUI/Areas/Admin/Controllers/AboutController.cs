using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.About;

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
        public IActionResult CreateAbout()
        {
            var values = _aboutService.TGetListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _aboutService.TInsertAsync(createAboutDto);
            return RedirectToAction("AboutList");
        }

        [HttpPost]

        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _aboutService.TDeleteAsync(id);
            return RedirectToAction("AboutList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id) { 
         var values = await _aboutService.TGetByIdAsync(id);

            var abaout = new UpdateAboutDto
            {
                AboutId = values.AboutId,
                Title = values.Title,
                Description = values.Description,
                FirstAdvantageTitle = values.FirstAdvantageTitle,
                FirstAdvantageDescription = values.FirstAdvantageDescription,
                SecondAdvantageTitle = values.SecondAdvantageTitle,
                SecondAdvantageDescription = values.SecondAdvantageDescription
            };
            return View(abaout);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto) { 
        
            await _aboutService.TUpdateAsync(updateAboutDto);
            return RedirectToAction("AboutList");
        }
    }
}
