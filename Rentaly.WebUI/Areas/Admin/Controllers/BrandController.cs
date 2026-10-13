using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> BrandList()
        {
            var values = await _brandService.TGetListAsync();
                return View(values);
            
        }

        [HttpGet]
        public async Task<IActionResult> CreateBrand()
        {
            var values = await _brandService.TGetListAsync();
            return View();
        }


    }
}
