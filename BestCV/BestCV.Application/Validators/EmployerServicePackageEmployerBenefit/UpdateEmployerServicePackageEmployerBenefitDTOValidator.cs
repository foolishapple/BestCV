using FluentValidation;
using BestCV.Application.Models.EmployerServicePackageEmployerBenefit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.EmployerServicePackageEmployerBenefit
{
    public class UpdateEmployerServicePackageEmployerBenefitDTOValidator : AbstractValidator<UpdateEmployerServicePackageEmployerBenefitDTO>
    {
        public UpdateEmployerServicePackageEmployerBenefitDTOValidator()
        {
            RuleFor(c => c.EmployerBenefitId).NotEmpty().WithMessage("Mã quyền lợi không được để trống.");
            RuleFor(c => c.EmployerServicePackageId).NotEmpty().WithMessage("Mã gói dịch vụ không được để trống.");
            RuleFor(c => c.Id).NotEmpty().WithMessage("Mã không được để trống.");
        }
    }
}
