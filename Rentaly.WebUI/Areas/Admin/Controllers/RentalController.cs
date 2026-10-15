
using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.RentalDtos;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;
        private readonly IEmailService _emailService;
        private readonly ILogger<RentalController> _logger;

        public RentalController(
            IRentalService rentalService,
            ICustomerService customerService,
            ICarService carService,
            IEmailService emailService,
            ILogger<RentalController> logger)
        {
            _rentalService = rentalService;
            _customerService = customerService;
            _carService = carService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<IActionResult> RentalList()
        {
            var values = await _rentalService.TGetListAsync();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRental(int id)
        {
            var rental = await _rentalService.TGetByIdAsync(id);

            if (rental == null)
            {
                TempData["Error"] = "Rezervasyon bulunamadı.";
                return RedirectToAction(nameof(RentalList));
            }

            if (await _rentalService.THasApprovedOverlapAsync(rental.CarId, rental.PickupDate, rental.ReturnDate, rental.RentalId))
            {
                TempData["Error"] = "Bu aracın seçilen tarihleri başka bir onaylı rezervasyonda dolu.";
                return RedirectToAction(nameof(RentalList));
            }

            await _rentalService.TUpdateAsync(new UpdateRentalDto
            {
                RentalId = rental.RentalId,
                CarId = rental.CarId,
                CustomerId = rental.CustomerId,
                PickupBranchId = rental.PickupBranchId,
                ReturnBranchId = rental.ReturnBranchId,
                PickupDate = rental.PickupDate,
                ReturnDate = rental.ReturnDate,
                TotalPrice = rental.TotalPrice,
                Status = "Onaylı"
            });

            // E-posta başarısız olsa bile rezervasyon onaylı kalır.
            try
            {
                var customer =
                    await _customerService.TGetByIdAsync(rental.CustomerId);

                var car =
                    await _carService.TGetByIdAsync(rental.CarId);

                if (customer == null ||
                    string.IsNullOrWhiteSpace(customer.Email))
                {
                    TempData["Warning"] =
                        "Rezervasyon onaylandı ancak müşterinin e-posta adresi bulunamadı.";

                    return RedirectToAction(nameof(RentalList));
                }

                var discountCode = GenerateDiscountCode();

                await _emailService.SendReservationApprovedEmailAsync(
                    customer.Email,
                    $"{customer.Name} {customer.Surname}".Trim(),
                    car?.DisplayName ?? "Araç",
                    rental.PickupDate,
                    rental.ReturnDate,
                    rental.TotalPrice,
                    discountCode);

                TempData["Success"] =
                    "Rezervasyon onaylandı ve müşteriye e-posta gönderildi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Rezervasyon {RentalId} onaylandı ancak e-posta gönderilemedi.",
                    rental.RentalId);

                TempData["Warning"] =
                    "Rezervasyon onaylandı ancak e-posta gönderilemedi.";
            }

            return RedirectToAction(nameof(RentalList));
        }

        private static string GenerateDiscountCode()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var code = new char[8];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
            }

            return $"RENTALY-{new string(code)}";
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRental(int id)
        {
            await _rentalService.TDeleteAsync(id);
            return RedirectToAction(nameof(RentalList));
        }

        [HttpGet]
        public async Task<IActionResult> CreateRental()
        {
            var values = await _rentalService.TGetListAsync();
            return View(values);
        }
    }
}
