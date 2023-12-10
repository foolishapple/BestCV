using BestCV.Domain.Aggregates.JobReasonsApply;
using BestCV.Domain.Aggregates.JobRequireSkill;
using BestCV.Domain.Aggregates.JobSecondaryPosition;
using BestCV.Domain.Aggregates.JobTag;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class JobDetailAggregates
    {
        public long Id { get; set; }
        public long RecruimentCampaignId { get; set; }
        public string RecruimentCampaignName { get; set; }
        public int JobStatusId { get; set; }
        public string JobStatusName { get; set; }
        public int JobPositionId { get; set; }
        public string JobPositionName { get; set; }
        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; }
        public string Name { get; set; } = null!;
        public string Search { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedTime { get; set; }
        public int TotalRecruitment { get; set; }
        public int GenderRequirement { get; set; }
        public int PrimaryJobCategoryId { get; set; }
        public string PrimaryJobCategoryName { get; set; } = null!;
        public int ExperienceRangeId { get; set; }
        public int Currency { get; set; }
        public int SalaryTypeId { get; set; }
        public int? SalaryFrom { get; set; }
        public int? SalaryTo { get; set; }
        public string? Overview { get; set; }
        public string Requirement { get; set; } = null!;
        public string Benefit { get; set; } = null!;
        public string? ReceiverName { get; set; }
        public string? ReceiverPhone { get; set; }
        public string? ReceiverEmail { get; set; }
        public DateTime? ApplyEndDate { get; set; }
        public int ViewCount { get; set; }
        public List<int> ListJobSecondaryJobCategory { get; set; } = new();
        public List<JobReasonApply> ListJobReasonApply { get; set; } = new();
        public List<JobWorkPlaceAggregates> ListJobRequireWorkplace { get; set; } = new();
        public List<int> ListJobRequireSkill { get; set; } = new();
        public List<int> ListTag { get; set; } = new();
    }
}
