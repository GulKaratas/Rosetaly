using FluentValidation;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.ValidationRules
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator() {
            RuleFor(x => x.PlateNumber)
                  .NotEmpty()
                  .WithMessage("Plaka bilgisi boş olamaz.");

            RuleFor(x => x.PlateNumber)
                .MinimumLength(5)
                .WithMessage("Plaka en az 5 karakter olmalıdır.");

            RuleFor(x => x.VIN)
                .NotEmpty()
                .WithMessage("Şasi numarası boş olamaz.");

            RuleFor(x => x.VIN)
                .Length(17)
                .WithMessage("Şasi numarası 17 karakter olmalıdır.");

            RuleFor(x => x.BrandId)
                .GreaterThan(0)
                .WithMessage("Marka seçilmelidir.");

            RuleFor(x => x.ModelId)
                .GreaterThan(0)
                .WithMessage("Model seçilmelidir.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Kategori seçilmelidir.");

            RuleFor(x => x.BranchId)
                .GreaterThan(0)
                .WithMessage("Şube seçilmelidir.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage("Geçerli bir araç yılı girilmelidir.");

            RuleFor(x => x.Kilometer)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Kilometre 0'dan küçük olamaz.");

            RuleFor(x => x.DailyPrice)
                .GreaterThan(0)
                .WithMessage("Günlük fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.DepositAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Depozito tutarı negatif olamaz.");

            RuleFor(x => x.SeatCount)
                .GreaterThan(0)
                .WithMessage("Koltuk sayısı 0'dan büyük olmalıdır.");

            RuleFor(x => x.LuggageCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Bagaj kapasitesi negatif olamaz.");

            RuleFor(x => x.FuelType)
                .NotEmpty()
                .WithMessage("Yakıt tipi boş olamaz.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage("Araç görseli boş olamaz.");
        }
    }
}
