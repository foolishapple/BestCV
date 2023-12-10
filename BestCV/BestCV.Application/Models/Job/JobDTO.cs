using BestCV.Application.Models.CandidateApplyJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Job
{
    public class JobDTO
    {
        public long Id { get; set; }
        public long RecruimentCampaignId { get; set; }
        public int JobStatusId { get; set; }
        public int JobCategoryId { get; set; }
        public int JobTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Search { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedTime { get; set; }
        public int TotalRecruitment { get; set; }
        public int GenderRequirement { get; set; }
        public int PrimaryJobPositionId { get; set; }
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
        public List<CandidateApplyJobDTO> ListCvs { get; set; } = new List<CandidateApplyJobDTO>();
    }
}
