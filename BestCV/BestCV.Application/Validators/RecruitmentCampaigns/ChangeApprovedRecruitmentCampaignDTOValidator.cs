using FluentValidation;
using BestCV.Application.Models.RecruitmentCampaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.RecruitmentCampaigns
{
    public class ChangeApprovedRecruitmentCampaignDTOValidator : AbstractValidator<ChangeApproveRecruitmentCampaignDTO>
    {
        public ChangeApprovedRecruitmentCampaignDTOValidator()
        {
            
        }
    }
}
