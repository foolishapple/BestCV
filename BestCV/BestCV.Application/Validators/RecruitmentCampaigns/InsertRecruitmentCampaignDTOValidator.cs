using FluentValidation;
using BestCV.Application.Models.RecruitmentCampaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.RecruitmentCampaigns
{
    public class InsertRecruitmentCampaignDTOValidator : AbstractValidator<InsertRecruitmentCampaignDTO>
    {
        public InsertRecruitmentCampaignDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(255).WithMessage("Tên có độ dài tối đã là 255 ký tự.");
            RuleFor(c => c.RecruitmentCampaignStatusId).NotEmpty().WithMessage("Trạng thái không được để trống.");
            RuleFor(c => c.Description).MaximumLength(500).WithMessage("Mô tả có độ dài tối đa là 500 ký tự.");
        }
    }
}
