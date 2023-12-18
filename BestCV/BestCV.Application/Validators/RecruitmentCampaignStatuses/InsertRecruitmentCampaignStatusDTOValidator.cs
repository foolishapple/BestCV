using FluentValidation;
using BestCV.Application.Models.RecruitmentCampaignStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.RecruitmentCampaignStatuses
{
    public class InsertRecruitmentCampaignStatusDTOValidator : AbstractValidator<InsertRecruitmentCampaignStatusDTO>
    {
        public InsertRecruitmentCampaignStatusDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255).WithMessage("Tên có độ dài tôi đa là 255 ký tự.");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
            RuleFor(c => c.Color).NotEmpty().WithMessage("Màu không được để trống.")
                .MaximumLength(12).WithMessage("Màu có độ dài tối đa là 12 ký tự");
        }
    }
}