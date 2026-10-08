using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultBrandComponentPartial : ViewComponent
    {
        private readonly IBrandService _brandService;

        public _DefaultBrandComponentPartial(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _brandService.TGetListAsync();
            return View(brands);
        }
    }
}
