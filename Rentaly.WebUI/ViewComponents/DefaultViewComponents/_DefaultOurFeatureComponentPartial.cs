using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultOurFeatureComponentPartial : ViewComponent
    {
        private readonly IOurFeatureService _ourFeatureService;
        public _DefaultOurFeatureComponentPartial(IOurFeatureService ourFeatureService)
        {
            _ourFeatureService = ourFeatureService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _ourFeatureService.TGetListAsync();
            return View(values);
        }
    }
}
