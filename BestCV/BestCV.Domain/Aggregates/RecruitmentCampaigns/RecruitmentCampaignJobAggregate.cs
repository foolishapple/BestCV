using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.RecruitmentCampaigns
{
    public class RecruitmentCampaignJobAggregate
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public string StatusColor { get; set; } = null!;
        public bool IsApproved { get; set; }
    }
}
