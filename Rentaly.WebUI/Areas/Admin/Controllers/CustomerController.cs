using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> CustomerList()
        {
            var values = await _customerService.TGetListAsync();
            return View(values);

        }

        [HttpGet]
        public async Task<IActionResult> CreateCustomer()
        {
            var values = await _customerService.TGetListAsync();
            return View();
        }


    }
}
