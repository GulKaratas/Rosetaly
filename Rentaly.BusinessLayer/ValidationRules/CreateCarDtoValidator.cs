using FluentValidation;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.EntityLayer.Entities;
using System;

namespace Rentaly.BusinessLayer.ValidationRules
{
    public class CreateCarDtoValidator : AbstractValidator<CreateCarDto>
    {
        public CreateCarDtoValidator()
        {
            // Plaka Bilgisi
            RuleFor(x => x.PlateNumber)
                .NotEmpty().WithMessage("Plaka bilgisi boş olamaz.")
                .MinimumLength(5).WithMessage("Plaka en az 5 karakter olmalıdır.")
                .MaximumLength(15).WithMessage("Plaka en fazla 15 karakter olmalıdır.");

            // Şasi Numarası (VIN) - Nullable olduğu için When kullanıldı
            When(x => !string.IsNullOrEmpty(x.VIN), () =>
            {
                RuleFor(x => x.VIN)
                    .Length(17).WithMessage("Şasi numarası (VIN) tam olarak 17 karakter olmalıdır.");
            });

            // İlişkili Id Tanımlamaları
            RuleFor(x => x.CarModelId)
                .GreaterThan(0).WithMessage("Lütfen geçerli bir araç modeli seçiniz.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Lütfen geçerli bir kategori seçiniz.");

            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("Lütfen geçerli bir şube seçiniz.");

            // Görsel URL - Nullable olduğu için When kullanıldı
            When(x => !string.IsNullOrEmpty(x.ImageUrl), () =>
            {
                RuleFor(x => x.ImageUrl)
                    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                    .WithMessage("Lütfen geçerli bir URL formatı giriniz.");
            });

            // Teknik Bilgiler
            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .WithMessage($"Araç yılı 1900 ile {DateTime.Now.Year + 1} arasında olmalıdır.");

            RuleFor(x => x.Kilometer)
                .GreaterThanOrEqualTo(0).WithMessage("Kilometre 0'dan küçük olamaz.");

            RuleFor(x => x.SeatCount)
                .GreaterThan(0).WithMessage("Koltuk sayısı en az 1 olmalıdır.");

            RuleFor(x => x.LuggageCount)
                .GreaterThanOrEqualTo(0).WithMessage("Bagaj kapasitesi negatif olamaz.");

            RuleFor(x => x.FuelType)
                .NotEmpty().WithMessage("Yakıt tipi boş olamaz.");

            RuleFor(x => x.Transmission)
                .NotEmpty().WithMessage("Vites tipi (Düz/Otomatik vb.) boş olamaz.");

            // Performans Bilgisi - Optional (Nullable)
            When(x => !string.IsNullOrEmpty(x.PerformanceInfo), () =>
            {
                RuleFor(x => x.PerformanceInfo)
                    .MaximumLength(100).WithMessage("Performans bilgisi en fazla 100 karakter olabilir.");
            });

            // Fiyatlandırma ve Finans
            RuleFor(x => x.DailyPrice)
                .GreaterThan(0).WithMessage("Günlük kiralama fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.DepositAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Depozito tutarı negatif olamaz.");

        }
    }
}
