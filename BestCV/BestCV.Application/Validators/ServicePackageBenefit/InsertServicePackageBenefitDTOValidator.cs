using FluentValidation;
using BestCV.Application.Models.ServicePackageBenefit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.ServicePackageBenefit
{
    public class InsertServicePackageBenefitDTOValidator : AbstractValidator<InsertServicePackageBenefitDTO>
    {
        public InsertServicePackageBenefitDTOValidator()
        {
            RuleFor(x=>x.EmployerServicePackageId).NotEmpty().WithMessage("Mã gói dịch vụ không được để trống");
            RuleFor(x=>x.BenefitId).NotEmpty().WithMessage("Mã quyền lợi không được để trống");
            //RuleFor(x=>x.Value).NotEmpty().WithMessage("Giá trị không được để trống");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Ghi chú không vượt quá 500 ký tự");
        }
    }
}
