using FluentValidation;
using BestCV.Application.Models.EmployerBenefit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.Benefit
{
    public class UpdateBenefitDTOValidator : AbstractValidator<UpdateBenefitDTO>
    {
        public UpdateBenefitDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Mã không được để trống.")
                .GreaterThan(0)
                .WithMessage("Mã phải lớn hơn 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255)
                .WithMessage("Tên không được vượt quá 255 ký tự");
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Ghi chú không được vượt quá 500 ký tự");
        }
    }
}
