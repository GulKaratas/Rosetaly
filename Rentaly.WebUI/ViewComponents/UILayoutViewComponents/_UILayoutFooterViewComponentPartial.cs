using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _UILayoutFooterViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
