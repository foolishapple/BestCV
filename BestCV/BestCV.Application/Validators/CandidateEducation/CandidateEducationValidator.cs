using FluentValidation;
using BestCV.Application.Models.CandidateEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandidateEducation
{
    public class CandidateEducationValidator : AbstractValidator<CandidateEducationDTO>
    {
        public CandidateEducationValidator() 
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Tên chuyên ngành không được để trống");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Tên chuyên ngành có độ dài tối đa 255 ký tự");

            RuleFor(x => x.School).NotEmpty().WithMessage("Tên trường học không được để trống");
            RuleFor(x => x.School).MaximumLength(255).WithMessage("Tên trường học có độ dài tối đa 255 ký tự");

            RuleFor(x => x.TimePeriod).NotEmpty().WithMessage("Thời gian học không được để trống");
            RuleFor(x => x.TimePeriod).MaximumLength(255).WithMessage("Thời gian học có độ dài tối đa 255 ký tự");


            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Mô tả có độ dài tối đa 500 ký tự");


        }
    }
}
