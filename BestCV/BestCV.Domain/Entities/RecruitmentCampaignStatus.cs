using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class RecruitmentCampaignStatus : EntityCommon<int>
    {
        public string Color { get; set; } = null!;

        public virtual ICollection<RecruitmentCampaign> RecruitmentCampaigns { get; } = new List<RecruitmentCampaign>();
    }
}
