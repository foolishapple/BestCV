using FluentValidation;
using BestCV.Application.Models.CandidateHonorAward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandidateHonorAward
{
    public class CandidateHonorAwardValidator : AbstractValidator<CandidateHonorAwardDTO>
    {     
        public CandidateHonorAwardValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên giải không được để trống");
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Tên giải có độ dài tối đa 255 ký tự");
          
            RuleFor(x => x.TimePeriod).NotEmpty().WithMessage("Thời gian không được để trống");
            RuleFor(x => x.TimePeriod).MaximumLength(255).WithMessage("Thời gian có độ dài tối đa 255 ký tự");

            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Mô tả có độ dài tối đa 500 ký tự");

        }
    }
}
