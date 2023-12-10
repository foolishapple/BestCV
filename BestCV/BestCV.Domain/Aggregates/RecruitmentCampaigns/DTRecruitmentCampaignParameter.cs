using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.RecruitmentCampaigns
{
    public class DTRecruitmentCampaignParameter : DTParameters
    {
        public long? EmployerId { get; set; }
        public int[] ReccruitmentCampaginStatusIds { get; set; } = new int[] { };
        public bool[] IsApproveds { get; set; } = new bool[] { };
    }
}
