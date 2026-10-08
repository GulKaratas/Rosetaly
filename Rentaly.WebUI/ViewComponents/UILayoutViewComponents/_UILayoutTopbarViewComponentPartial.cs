using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _UILayoutTopbarViewComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
