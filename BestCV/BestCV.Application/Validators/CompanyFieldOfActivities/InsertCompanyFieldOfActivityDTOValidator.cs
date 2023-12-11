using FluentValidation;
using BestCV.Application.Models.CompanyFieldOfActivities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CompanyFieldOfActivities
{
	public class InsertCompanyFieldOfActivityDTOValidator : AbstractValidator<InsertCompanyFieldOfActivityDTO>
	{
        public InsertCompanyFieldOfActivityDTOValidator()
        {
            RuleFor(c => c.CompanyId).NotEmpty().WithMessage("Mã công ty không được để trống.");
            RuleFor(c => c.FieldOfActivityId).NotEmpty().WithMessage("Mã lĩnh vực hoạt động không được để trống.");
        }
    }
}
