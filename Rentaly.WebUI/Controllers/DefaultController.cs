using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
