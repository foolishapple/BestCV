using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.JobRequireCity;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.TopFeatureJob
{
    public class TopFeatureJobAggregates : EntityBase<int>
    {
        public long TopFeatureJobId { get; set; }
        public string TopFeatureJobName { get; set; }
        public int OrderSort { get; set; }
        public int SubOrderSort { get; set; }
        public long RecruimentCampaignId { get; set; }
        public int JobStatusId { get; set; }

        public int TotalRecruitment { get; set; }

        public int GenderRequirement { get; set; }

        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; }
        public int JobPositionId { get; set; }
        public bool IsApproved { get; set; }
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

        public DateTime? ApprovalDate { get; set; }
        public int PrimaryJobCategoryId { get; set; }
        public string PrimaryJobCategoryName { get; set; }
        public string PrimaryJobCategoryIcon { get; set; }
        public DateTime JobCreatedTime { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public List<JobRequireCityAggregates> JobRequireCity { get; set; }
        public List<Benefit> ListBenefit { get; set; }
        public bool IsSaveJob { get; set; }
        public DateTime? RefreshDate { get; set; }
    }
}
