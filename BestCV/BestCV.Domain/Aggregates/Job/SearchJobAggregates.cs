using BestCV.Domain.Aggregates.JobRequireCity;
using BestCV.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class SearchJobAggregates
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public string Description { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        [JsonIgnore]
        public bool Active { get; set; }
        [JsonIgnore]
        public string Search { get; set; }
        [JsonIgnore]
        public long RecruimentCampaignId { get; set; }
        public int JobPositionId { get; set; }
        public string JobPositionName { get; set; } = null!;
        public int JobStatusId { get; set; }
        public string JobStatusName { get; set; } = null!;
        public int TotalRecruitment { get; set; }
        [JsonIgnore]
        public int GenderRequirement { get; set; }
        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; } = null!;

        public int PrimaryJobCategoryId { get; set; }
        public string PrimaryJobCategoryName { get; set; } = null!;
        public bool IsApproved { get; set; }
        public int ExperienceRangeId { get; set; }
        public string ExperienceRangeName { get; set; } = null!;
        public int Currency { get; set; }
        public int SalaryTypeId { get; set; }
        public string SalaryTypeName { get; set; } = null!;
        public int? SalaryFrom { get; set; }
        public int? SalaryTo { get; set; }
        [JsonIgnore]
        public string? Overview { get; set; }
        [JsonIgnore]
        public string Requirement { get; set; } = null!;
        [JsonIgnore]
        public string Benefit { get; set; } = null!;
        [JsonIgnore]
        public string? ReceiverName { get; set; }
        [JsonIgnore]
        public string? ReceiverPhone { get; set; }
        [JsonIgnore]
        public string? ReceiverEmail { get; set; }
        public DateTime? ApplyEndDate { get; set; }
        [JsonIgnore]
        public DateTime? ApprovalDate { get; set; }
        [JsonIgnore]
        public long EmloyerId { get; set; }
        [JsonIgnore]
        public string EmployerName { get; set; } = null!;
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string CompanyLogo { get; set; } = null!;
        public List<JobRequireCityAggregates> JobRequireCity { get; set; }
        public List<int> ListBenefit { get; set; }
        public bool IsSaveJob { get; set; }
        public DateTime? RefreshDate { get; set; }
    }
}
