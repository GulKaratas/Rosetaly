using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/NotFound")]
        public IActionResult NotFoundPage(int statusCode)
        {
            // Doğru ve direkt fiziksel dosya yolu:
            return View("~/Views/Error/NotFound.cshtml");
        }
    }
}