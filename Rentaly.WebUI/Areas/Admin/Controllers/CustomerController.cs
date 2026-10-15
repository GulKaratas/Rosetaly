using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CustomerDtos;

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

        // Listeleme
        public async Task<IActionResult> CustomerList()
        {
            var values = await _customerService.TGetListAsync();
            return View(values);
        }

        // Ekleme (GET)
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        // Ekleme (POST)
        [HttpPost]
       
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createCustomerDto);
            }

            await _customerService.TInsertAsync(createCustomerDto);
            return RedirectToAction("CustomerList");
        }

        // Güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var value = await _customerService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound();
            }

            // Entity'den Update DTO'ya veya doğrudan DTO verisine aktarım
            var model = new UpdateCustomerDto
            {
                CustomerId = value.CustomerId,
                Name = value.Name,
                Surname = value.Surname,
                Email = value.Email,
                Phone = value.Phone,
                IdentityNumber = value.IdentityNumber,
                DrivingLicenseDate = value.DrivingLicenseDate,
                DrivingLicenseNumber     = value.DrivingLicenseNumber
            };

            return View(model);
        }

        // Güncelleme (POST)
        [HttpPost]
     
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
           

            await _customerService.TUpdateAsync(updateCustomerDto);
            return RedirectToAction("CustomerList");
        }

        // Silme (POST)
        [HttpPost]
        
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.TDeleteAsync(id);
            return RedirectToAction("CustomerList");
        }
    }
}