using FluentValidation;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.ValidationRules
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator() {

            // Marka adı boş olamaz
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage("Marka adı boş geçilemez");

            // Minimum karakter
            RuleFor(x => x.BrandName)
                .MinimumLength(2).WithMessage("Marka adı en az 2 karakter olmalıdır");

            // Maximum karakter
            RuleFor(x => x.BrandName)
                .MaximumLength(30).WithMessage("Marka adı en fazla 30 karakter olabilir");

            // Sadece harf içermesi
            RuleFor(x => x.BrandName)
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s\-]+$")
                .WithMessage("Marka adı sadece harf içermelidir");

          

        }
    }
}
