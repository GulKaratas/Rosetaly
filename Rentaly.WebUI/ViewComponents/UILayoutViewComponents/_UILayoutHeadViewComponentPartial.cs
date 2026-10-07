using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _UILayoutHeadViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
