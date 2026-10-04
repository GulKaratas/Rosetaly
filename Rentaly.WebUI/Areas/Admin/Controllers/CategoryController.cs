using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }
    }
}
