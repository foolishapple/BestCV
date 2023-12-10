using BestCV.Domain.Aggregates.Job;
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

namespace BestCV.Application.Models.Job
{
    public class UpdateJobDTO
    {
        public long Id { get; set; }
        public long RecruimentCampaignId { get; set; }
        public int JobStatusId { get; set; }
        public int JobPositionId { get; set; }
        public int JobTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Search { get; set; } = null!;
        public string? Description { get; set; }
        public int TotalRecruitment { get; set; }
        public int GenderRequirement { get; set; }
        public int PrimaryJobCategoryId { get; set; }
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

        /// <summary>
        /// Ngành nghề phụ
        /// </summary>
        public List<int> ListJobSecondaryJobCategory { get; set; } = new();

        /// <summary>
        /// lý do ứng tuyển
        /// </summary>
        public List<JobReasonApplyAggregates> ListJobReasonApply { get; set; } = new();

        /// <summary>
        /// Nơi làm việc
        /// </summary>
        public List<JobWorkPlaceDTO> ListJobRequireWorkplace { get; set; } = new();


        /// <summary>
        /// Kỹ năng yêu cầu
        /// </summary>
        public List<int> ListJobRequireSkill { get; set; } = new();

        /// <summary>
        /// Mã thẻ của tin tuyển dụng
        /// </summary>
        public List<int> ListTag { get; set; } = new();
    }
}
