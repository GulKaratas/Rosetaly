using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _UILayoutScriptsViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
