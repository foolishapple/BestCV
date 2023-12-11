using FluentValidation;
using BestCV.Application.Models.CandidateWorkExperience;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandateWorkExperience
{
    public class CandidateWorkExperienceValidator : AbstractValidator<CandidateWorkExperienceDTO>
    {
        public CandidateWorkExperienceValidator() 
        {
            RuleFor(x => x.JobTitle).NotEmpty().WithMessage("Tên chức vụ không được để trống");
            RuleFor(x => x.JobTitle).MaximumLength(255).WithMessage("Tên chức vụ có độ dài tối đa 255 ký tự");

            RuleFor(x => x.Company).NotEmpty().WithMessage("Tên công ty không được để trống");
            RuleFor(x => x.Company).MaximumLength(255).WithMessage("Tên chức vụ có độ dài tối đa 255 ký tự");

            RuleFor(x => x.TimePeriod).NotEmpty().WithMessage("Thời gian làm việc không được để trống");
            RuleFor(x => x.TimePeriod).MaximumLength(255).WithMessage("Thời gian làm việc có độ dài tối đa 255 ký tự");

            
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Mô tả có độ dài tối đa 500 ký tự");

        }
    }
}
