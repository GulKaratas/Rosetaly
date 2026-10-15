using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.DtoLayer.RentalDtos;
using Rentaly.WebUI.Models;

namespace Rentaly.WebUI.Controllers
{
    public class RentalController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBranchService _branchService;
        private readonly ICustomerService _customerService;
        private readonly IRentalService _rentalService;

        public RentalController(
            ICarService carService,
            IBranchService branchService,
            ICustomerService customerService,
            IRentalService rentalService)
        {
            _carService = carService;
            _branchService = branchService;
            _customerService = customerService;
            _rentalService = rentalService;
        }

        public async Task<IActionResult> Index(int id)
        {
            return View(await BuildPageAsync(id, new ReservationForm { CarId = id }));
        }

        [HttpPost]
       
        public async Task<IActionResult> Index(ReservationForm form)
        {
            var page = await BuildPageAsync(form.CarId, form);
            if (page.Car == null)
            {
                return View(page);
            }

            if (!TryBuildSchedule(form, out var pickup, out var dropoff, out var error, out var step))
            {
                page.ErrorMessage = error;
                page.ActiveStep = step;
                return View(page);
            }

            if (!page.Branches.Any(x => x.BranchId == form.PickupBranch) ||
                !page.Branches.Any(x => x.BranchId == form.ReturnBranch))
            {
                page.ErrorMessage = "Şube seçimi geçersiz.";
                page.ActiveStep = 1;
                return View(page);
            }

            if (!IsDriverValid(form, out error))
            {
                page.ErrorMessage = error;
                page.ActiveStep = 2;
                return View(page);
            }

            if (await _rentalService.THasApprovedOverlapAsync(page.Car.CarId, pickup, dropoff))
            {
                page.ErrorMessage = "Bu araç seçilen tarihlerde onaylı bir rezervasyona ayrılmış.";
                page.ActiveStep = 1;
                return View(page);
            }

            var days = Math.Max(1, (int)Math.Ceiling((dropoff - pickup).TotalDays));
            var customerId = await FindOrCreateCustomerAsync(form);
            await _rentalService.TInsertAsync(new CreateRentalDto
            {
                CarId = page.Car.CarId,
                CustomerId = customerId,
                PickupBranchId = form.PickupBranch,
                ReturnBranchId = form.ReturnBranch,
                PickupDate = pickup,
                ReturnDate = dropoff,
                TotalPrice = days * page.Car.DailyPrice,
                Status = "Bekliyor"
            });

            page.Submitted = true;
            return View(page);
        }

        private async Task<ReservationPageViewModel> BuildPageAsync(int carId, ReservationForm form)
        {
            var car = await _carService.TGetByIdAsync(carId);
            return new ReservationPageViewModel
            {
                Car = car != null && car.IsAvailable ? car : null,
                Branches = await _branchService.TGetListAsync(),
                Form = form
            };
        }

        private static bool TryBuildSchedule(
            ReservationForm form,
            out DateTime pickup,
            out DateTime dropoff,
            out string error,
            out int step)
        {
            pickup = default;
            dropoff = default;
            error = "";
            step = 1;

            if (!DateTime.TryParseExact(form.PickupDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var pickupDay)
                || !TimeSpan.TryParse(form.PickupTime, out var pickupTime))
            {
                error = "Alış tarihi ve saati gerekli.";
                return false;
            }

            if (!DateTime.TryParseExact(form.ReturnDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var returnDay)
                || !TimeSpan.TryParse(form.ReturnTime, out var returnTime))
            {
                error = "İade tarihi ve saati gerekli.";
                return false;
            }

            pickup = pickupDay.Date.Add(pickupTime);
            dropoff = returnDay.Date.Add(returnTime);

            if (pickup.Date < DateTime.Today)
            {
                error = "Geçmiş bir tarih seçilemez.";
                return false;
            }

            if (dropoff <= pickup)
            {
                error = "İade, alış tarihinden sonra olmalıdır.";
                return false;
            }

            return true;
        }

        private static bool IsDriverValid(ReservationForm form, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(form.FirstName) || string.IsNullOrWhiteSpace(form.LastName))
            {
                error = "Ad ve soyad gerekli.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(form.Email) || !form.Email.Contains('@') || !form.Email.Contains('.'))
            {
                error = "Geçerli bir e-posta girin.";
                return false;
            }

            var digits = new string((form.Phone ?? "").Where(char.IsDigit).ToArray());
            if (digits.Length < 10)
            {
                error = "Telefon numarası gerekli.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(form.NationalId) || form.NationalId.Trim().Length != 11 || form.NationalId.Trim().Any(ch => !char.IsDigit(ch)))
            {
                error = "Kimlik numarası 11 hane olmalıdır.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(form.LicenseNo)
                || !DateTime.TryParseExact(form.LicenseDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var licenseDate)
                || licenseDate.Date > DateTime.Today)
            {
                error = "Ehliyet numarası ve tarihi gerekli.";
                return false;
            }

            return true;
        }

        private async Task<int> FindOrCreateCustomerAsync(ReservationForm form)
        {
            var email = form.Email.Trim();
            var customers = await _customerService.TGetListAsync();
            var existing = customers.FirstOrDefault(x =>
                string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                return existing.CustomerId;
            }

            await _customerService.TInsertAsync(new CreateCustomerDto
            {
                Name = form.FirstName.Trim(),
                Surname = form.LastName.Trim(),
                Email = email,
                Phone = form.Phone.Trim(),
                IdentityNumber = form.NationalId.Trim(),
                DrivingLicenseNumber = form.LicenseNo.Trim(),
                DrivingLicenseDate = DateTime.ParseExact(form.LicenseDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
            });

            customers = await _customerService.TGetListAsync();
            return customers
                .Where(x => string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.CustomerId)
                .First()
                .CustomerId;
        }
    }
}
