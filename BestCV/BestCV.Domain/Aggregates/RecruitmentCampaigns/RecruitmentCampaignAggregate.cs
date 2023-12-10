using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.RecruitmentCampaigns
{
    public class RecruitmentCampaignAggregate
    {
        public long Id { get; set; }
        public long EmployerId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string StatusName { get; set; } = null!;
        public string StatusColor { get; set; } = null!;
        public int TotalCandidate { get; set; }
        public int TotalJob { get; set; }
        public bool IsApproved { get; set; }
        public List<RecruitmentCampaignCandidateAggregate> Candidates { get; set; } = new();
        public List<RecruitmentCampaignJobAggregate> Jobs { get; set; } = new();
        public int RecruitmetnCampaginStatusId { get; set; }
    }
}
