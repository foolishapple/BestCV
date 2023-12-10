using BestCV.Domain.Aggregates.CompanyFieldOfActivity;
using BestCV.Domain.Aggregates.JobRequireCity;
using BestCV.Domain.Aggregates.JobRequireJobSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class DetailJobAggregates
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public bool Active { get; set; }
        public string Search { get; set; }
        public long RecruimentCampaignId { get; set; }
        public int PrimaryJobCategoryId { get; set; }
        public string PrimaryJobCategoryName { get; set; } = null!;
        public string PrimaryJobCategoryIcon { get; set; } = null!;
        public int JobStatusId { get; set; }
        public string JobStatusName { get; set; } = null!;
        public int TotalRecruitment { get; set; }
        public int GenderRequirement { get; set; }
        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; } = null!;

        public int JobPositionId { get; set; }
        public string JobPositionName { get; set; } = null!;
        public bool IsApproved { get; set; }
        public int ExperienceRangeId { get; set; }
        public string ExperienceRangeName { get; set; } = null!;
        public int Currency { get; set; }
        public int SalaryTypeId { get; set; }
        public string SalaryTypeName { get; set; } = null!;
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
        public long EmloyerId { get; set; }
        public string EmployerName { get; set; } = null!;
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string CompanyLogo { get; set; } = null!;
        public int CompanySizeId { get; set; }
        public string CompanySizeName { get; set; } = null;
        public string CompanyAddressDetail { get; set; } = null!;
        public string CompanyWebsite { get; set; } = null!;
        public string CompanyPhone { get; set; } = null!;
        public string CompanyCoverPhoto { get; set; } = null!;
        public string CompanyTaxCode { get; set; } = null!;
        public int CompanyFoundedIn { get; set; }
        public string CompanyTiktokLink { get; set; } = null!;
        public string CompanyYoutubeLink { get; set; } = null!;
        public string CompanyFacebookLink { get; set; } = null!;
        public string CompanyLinkedInLink { get; set; } = null!;
        public string CompanyDescription { get; set; } = null!;
        public string CompanyEmailAddress { get; set; } = null!;
        public string CompanyWorkPlace { get; set; } = null!;
        public int? CompanyWorkPlaceId { get; set; }

        public List<JobRequireCityAggregates> JobRequireCity { get; set; }
        public List<JobRequireJobSkillAggregates> JobRequireSkill { get; set; }
        public List<CompanyFieldOfActivityAggregates> FieldOfCompany { get; set; }
        public bool IsSaveJob { get; set; }
        public bool IsApplyJob { get; set; }
    }
}
