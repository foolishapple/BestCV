using FluentValidation;
using BestCV.Application.Models.CandidateActivites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandidateActivites
{
    public class CandidateActivitesValidator : AbstractValidator<CandidateActivitesDTO>
    {
        public CandidateActivitesValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên hoạt động không được để trống");
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Tên hoạt động có độ dài tối đa 255 ký tự");         

            RuleFor(x => x.TimePeriod).NotEmpty().WithMessage("Thời gian không được để trống");
            RuleFor(x => x.TimePeriod).MaximumLength(255).WithMessage("Thời gian có độ dài tối đa 255 ký tự");

            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Mô tả có độ dài tối đa 500 ký tự");
        }
    }
}
