using FluentValidation;
using BestCV.Application.Models.CandidateCertificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandidateCertificate
{
    public class CandidateCertificateValidator : AbstractValidator<CandidateCertificateDTO>
    {
    public CandidateCertificateValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên chứng chỉ không được để trống");
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Tên chứng chỉ có độ dài tối đa 255 ký tự");

            RuleFor(x => x.IssueBy).NotEmpty().WithMessage("Tên nơi cấp không được để trống");
            RuleFor(x => x.IssueBy).MaximumLength(255).WithMessage("Tên nơi cấp có độ dài tối đa 255 ký tự");

            RuleFor(x => x.TimePeriod).NotEmpty().WithMessage("Thời gian không được để trống");
            RuleFor(x => x.TimePeriod).MaximumLength(255).WithMessage("Thời gian có độ dài tối đa 255 ký tự");

            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Mô tả có độ dài tối đa 500 ký tự");
        }
    }
}
