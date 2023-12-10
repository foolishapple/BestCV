using BestCV.Application.Models.CandidateApplyJobs;
using BestCV.Application.Models.Candidates;
using BestCV.Application.Models.Company;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Job
{
    public class JobDetailDTO
    {
        public long Id { get; set; }
        public long RecruimentCampaignId { get; set; }
        public string RecruimentCampaignName { get; set; }
        public int JobStatusId { get; set; }
        public int JobPositionId { get; set; }
        public int JobTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Search { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedTime { get; set; }
        public int TotalRecruitment { get; set; }
        public int GenderRequirement { get; set; }
        public int PrimaryJobCategoryId { get; set; }
        public int ExperienceRangeId { get; set; }
        public int Currency { get; set; }
        public int SalaryTypeId { get; set; }
        public int? SalaryFrom { get; set; }
        public int? SalaryTo { get; set; }
        public string FormattedSalary { get; set; }
        public string? Overview { get; set; }
        public string Requirement { get; set; } = null!;
        public string Benefit { get; set; } = null!;
        public string? ReceiverName { get; set; }
        public string? ReceiverPhone { get; set; }
        public string? ReceiverEmail { get; set; }
        public DateTime? ApplyEndDate { get; set; }

        /// <summary>
        /// List Ngành nghề phụ
        /// </summary>
        public List<int> ListJobSecondaryCategories { get; set; } = new();

        /// <summary>
        /// List lý do ứng tuyển
        /// </summary>
        public List<string> ListJobReasonApply { get; set; } = new();

        /// <summary>
        /// List Nơi làm việc
        /// </summary>
        public List<JobWorkPlaceDTO> ListJobRequireWorkplace { get; set; } = new();

        /// <summary>
        /// List Kỹ năng yêu cầu
        /// </summary>
        public List<int> ListJobRequireJobSkill { get; set; } = new();

        /// <summary>
        /// List Mã thẻ của tin tuyển dụng
        /// </summary>
        public List<int> ListTag { get; set; } = new();
        public List<CandidateApplyJobDTO> ListCvs { get; set; } = new List<CandidateApplyJobDTO>();
        public List<CompanyDTO> Company { get; set; } = new List<CompanyDTO>();

    }
}
