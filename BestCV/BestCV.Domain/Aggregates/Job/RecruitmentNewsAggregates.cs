using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class RecruitmentNewsAggregates
    {
        public long Id { get; set; }
        public long RecruimentCampaignId { get; set; }
        public string RecruimentCampaignName { get; set; }
        public string CompanyName { get; set; }
        public int JobStatusId { get; set; }
        public string JobStatusName { get; set; }
        public int JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }
        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; }
        public string Name { get; set; } = null!;
        public string Search { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
