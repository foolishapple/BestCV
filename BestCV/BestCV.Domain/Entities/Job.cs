using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class Job : EntityFullTextSearch<long>
    {
        /// <summary>
        /// Mã chiến dịch tuyển dụng
        /// </summary>
        public long RecruimentCampaignId { get; set; }

        /// <summary>
        /// Mã trạng thái tin tuyển dụng
        /// </summary>
        public int JobStatusId { get; set; }


        /// <summary>
        /// Số lượng tuyển
        /// </summary>
        public int TotalRecruitment { get; set; }

        /// <summary>
        /// Giới tính yêu cầu
        /// </summary>
        public int GenderRequirement { get; set; }

        /// <summary>
        /// Mã loại tin tuyển dụng
        /// </summary>
        public int JobTypeId { get; set; }

        /// <summary>
        /// Mã ngành nghề tuyển dụng chính
        /// </summary>
        public int PrimaryJobCategoryId { get; set; }

        /// <summary>
        /// Tin tuyển dụng có được duyệt không
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Khoảng kinh nghiệm yêu cầu
        /// </summary>
        public int ExperienceRangeId { get; set; }

        /// <summary>
        /// Loại tiền tệ
        /// </summary>
        public int Currency { get; set; }

        /// <summary>
        /// Mã loại lương
        /// </summary>
        public int SalaryTypeId { get; set; }

        /// <summary>
        /// Lương tối thiểu
        /// </summary>
        public int? SalaryFrom { get; set; }

        /// <summary>
        /// Lương tối đa
        /// </summary>
        public int? SalaryTo { get; set; }

        /// <summary>
        /// Tổng quan công việc
        /// </summary>
        public string? Overview { get; set; }

        /// <summary>
        /// Yêu cầu công việc
        /// </summary>
        public string Requirement { get; set; } = null!;

        /// <summary>
        /// Lợi ích
        /// </summary>
        public string Benefit { get; set; } = null!;

        /// <summary>
        /// Tên người nhận
        /// </summary>
        public string? ReceiverName { get; set; }

        /// <summary>
        /// Điện thoại người nhận
        /// </summary>
        public string? ReceiverPhone { get; set; }

        /// <summary>
        /// Email người nhận
        /// </summary>
        public string? ReceiverEmail { get; set; }

        /// <summary>
        /// Thời hạn ứng tuyển
        /// </summary>
        public DateTime? ApplyEndDate { get; set; }

        /// <summary>
        /// Thời điểm duyệt tin
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
        /// <summary>
        /// Mã ngành nghề của tin tuyển dụng
        /// </summary>
        public int JobPositionId { get; set; }
        /// <summary>
        /// Số lượt xem
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// Ngày làm mới
        /// </summary>
        public DateTime? RefreshDate { get; set; }
        [JsonIgnore]
        public virtual JobCategory PrimaryJobCategory { get; } = null!;
        [JsonIgnore]
        public virtual RecruitmentCampaign RecruitmentCampaign { get; } = null!;
        [JsonIgnore]
        public virtual JobStatus JobStatus { get; } = null!;
        [JsonIgnore]
        public virtual JobType JobType { get; } = null!;
        [JsonIgnore]
        public virtual ExperienceRange ExperienceRange { get; } = null!;
        [JsonIgnore]
        public virtual SalaryType SalaryType { get; } = null!;
        [JsonIgnore]
        public virtual ICollection<TopFeatureJob> TopFeatureJobs { get; } = new List<TopFeatureJob>();
        [JsonIgnore] 
        public virtual ICollection<JobReasonApply> JobReasonApplies { get; } = new List<JobReasonApply>();
        [JsonIgnore] 
        public virtual ICollection<JobSecondaryJobCategory> JobSecondaryJobCategories { get; } = new List<JobSecondaryJobCategory>();
        [JsonIgnore] 
        public virtual ICollection<JobMultimedia> JobMultimedias { get; } = new List<JobMultimedia>();
        [JsonIgnore] 
        public virtual ICollection<JobRequireJobSkill> JobRequireJobSkills { get; } = new List<JobRequireJobSkill>();
        [JsonIgnore] 
        public virtual ICollection<JobRequireCity> JobRequireCities { get; } = new List<JobRequireCity>();
        [JsonIgnore] 
        public virtual JobPosition JobPosition { get; } = null!;
        [JsonIgnore] 
        public virtual ICollection<JobTag> JobTags { get; } = new List<JobTag>();
        [JsonIgnore]
        public virtual ICollection<CandidateSaveJob> CandidateSaveJobs { get; } = new List<CandidateSaveJob>();
        [JsonIgnore]
        public virtual ICollection<CandidateIgnoreJob> CandidateIgnoreJobs { get; } = new List<CandidateIgnoreJob>();
        [JsonIgnore]
        public virtual ICollection<CandidateApplyJob> CandidateApplyJobs { get; } = new List<CandidateApplyJob>();
        [JsonIgnore]
        public virtual ICollection<TopJobManagement> TopJobManagements { get; } = new List<TopJobManagement>(); 
        [JsonIgnore]
        public virtual ICollection<JobReference> JobReferences { get; } = new List<JobReference>();
        [JsonIgnore]
        public virtual ICollection<JobSuitable> JobSuitables { get; } = new List<JobSuitable>();
        [JsonIgnore]
        public virtual ICollection<TopJobUrgent> TopJobUrgents { get; set; } = new List<TopJobUrgent>();
        [JsonIgnore]
        public virtual ICollection<TopJobExtra> TopJobExtras { get; set; } = new List<TopJobExtra>();
        [JsonIgnore]
        public virtual ICollection<JobServicePackage> JobServicePackages { get; set; } = new List<JobServicePackage>();
        [JsonIgnore]
        public virtual ICollection<CandidateViewedJob> CandidateViewedJobs { get; } = new List<CandidateViewedJob>();
        [JsonIgnore]
        public virtual ICollection<TopAreaJob> TopAreaJobs { get; } = new List<TopAreaJob>();
        [JsonIgnore]
        public virtual ICollection<RefreshJob> RefreshJobs { get; } = new List<RefreshJob>();
    }
}
