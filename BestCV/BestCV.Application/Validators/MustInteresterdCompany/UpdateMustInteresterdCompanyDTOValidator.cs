using FluentValidation;
using BestCV.Application.Models.MustBeInterestedCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.MustInteresterdCompany
{
    public class UpdateMustInteresterdCompanyDTOValidator : AbstractValidator<UpdateMustBeInterestedCompanyDTO>
    {
        public UpdateMustInteresterdCompanyDTOValidator()
        {
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
        }
    }
}
