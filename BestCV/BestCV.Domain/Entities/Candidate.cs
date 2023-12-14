using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class Candidate : EntityBase<long>, IFullTextSearch
    {
        public int CandidateStatusId { get; set; }
        public int CandidateLevelId { get; set; }
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// Mã google
        /// </summary>
        public string? GoogleId { get; set; }

        /// <summary>
        /// Mã fb
        /// </summary>
        public string? FacebookId { get; set; }
        /// <summary>
        /// Mã linked
        /// </summary>
        public string? LinkedinId { get; set; }
        public string? Photo { get; set; }
        public string? CoverPhoto { get; set; }
        public int Gender { get; set; }
        /// <summary>
        /// Vị trí công việc
        /// </summary>
        public string? JobPosition { get; set; }
        /// <summary>
        /// Chi tiết địa chỉ
        /// </summary>
        public string? AddressDetail { get; set; }
        /// <summary>
        /// Sở thích
        /// </summary>
        public string? Interests { get; set; }
        /// <summary>
        /// Mục tiêu
        /// </summary>
        public string? Objective { get; set; }
        public string? Info { get; set; }
        public DateTime? DoB { get; set; }
        /// <summary>
        /// Quốc tịch
        /// </summary>
        public string? Nationality { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string? MaritalStatus { get; set; } 
        /// <summary>
        /// Người tham khảo
        /// </summary>
        public string? References { get; set; }
        public string Phone { get; set; }
        /// <summary>
        ///
        /// </summary>
        public bool IsSubcribeEmailImportantSystemUpdate { get; set; }
        /// <summary>
        /// Cho nhà tuyển dụng view CV
        /// </summary>
        public bool IsSubcribeEmailEmployerViewCV { get; set; }
        /// <summary>
        /// Nhận thông báo email về cập nhật tính năng mới
        /// </summary>
        public bool IsSubcribeEmailNewFeatureUpdate { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về hệ thống khác
        /// </summary>
        public bool IsSubcribeEmailOtherSystemNotification { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về gợi ý công việc
        /// </summary>
        public bool IsSubcribeEmailJobSuggestion { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về nhà tuyển dụng mời làm việc
        /// </summary>
        public bool IsSubcribeEmailEmployerInviteJob { get; set; }
        /// <summary>
        /// Nhận thông báo qua email giới thiệu về dịch vụ
        /// </summary>
        public bool IsSubcribeEmailServiceIntro { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về chương trình sự kiện giới thiệu
        /// </summary>
        public bool IsSubcribeEmailProgramEventIntro { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về gift phiếu giảm giá
        /// </summary>
        public bool IsSubcribeEmailGiftCoupon { get; set; }
        /// <summary>
        /// Kiểm tra công việc đang chờ
        /// </summary>
        public bool IsCheckOnJobWatting { get; set; }
        /// <summary>
        /// Kiểm tra mời làm việc
        /// </summary>
        public bool IsCheckJobOffers { get; set; }
        /// <summary>
        /// Kiểm tra xem sơ yếu lí lịch
        /// </summary>
        public bool IsCheckViewCV { get; set; }
        /// <summary>
        /// Kiểm tra đánh giá TopCV
        /// </summary>
        public bool IsCheckTopCVReview { get; set; }
        /// <summary>
        /// Kinh nghiệm làm việc
        /// </summary>
        public int SuggestionExperienceRangeId { get; set; }
        /// <summary>
        /// Mức lương
        /// </summary>
        public int SuggestionSalaryRangeId { get; set; }
        /// <summary>
        /// Số lần đăng nhập thất bại
        /// </summary>
        public int AccessFailedCount { get; set; }
        /// <summary>
        /// Bị khoá tài khoản?
        /// </summary>
        public bool LockEnabled { get; set; }
        /// <summary>
        /// Bị khóa đến thời gian nào
        /// </summary>
        public DateTime? LockEndDate { get; set; }
        /// <summary>
        /// Hiệu lực của cấp độ ứng viên
        /// </summary>
        public DateTime? CandidateLevelEfficiencyExpiry { get; set; }
        /// <summary>
        /// Đã xác thực chưa
        /// </summary>
        public bool IsActivated { get; set; }
        public string Search { get; set; }
        public virtual AccountStatus CandidateStatuses { get; }
        public virtual CandidateLevel CandidateLevels { get; }

        public ICollection<CandidateWorkExperience> CandidateWorkExperiences { get; } = new List<CandidateWorkExperience>();

        public ICollection<CandidateEducation> CandidateEducations { get; } = new List<CandidateEducation>();

        public ICollection<CandidateCertificate> CandidateCertificates { get; } = new List<CandidateCertificate>();

        public ICollection<CandidateHonorAndAward> CandidateHonorAndAwards { get; } = new List<CandidateHonorAndAward>();

        public ICollection<CandidateActivities> CandidateActivities { get; } = new List<CandidateActivities>();
        public ICollection<CandidateProjects> CandidateProjectses { get; } = new List<CandidateProjects>();
        public ICollection<CandidateMeta> CandidateMetas { get; } = new List<CandidateMeta>();
        public ICollection<CandidateCoupon> CandidateCoupons { get; } = new List<CandidateCoupon>();
        public ICollection<CandidatePassword> CandidatePasswords { get; } = new List<CandidatePassword>();
        public ICollection<CandidateSuggestionWorkPlace> CandidateSuggestionWorkPlaces { get; } = new List<CandidateSuggestionWorkPlace>();
        public ICollection<CandidateSuggestionJobPosition> CandidateSuggestionJobPositions { get; } = new List<CandidateSuggestionJobPosition>();
        public ICollection<CandidateSuggestionJobCategory> CandidateSuggestionJobCategories { get; } = new List<CandidateSuggestionJobCategory>();
        public ICollection<CandidateSuggestionJobSkill> CandidateSuggestionJobSkills { get; } = new List<CandidateSuggestionJobSkill>();
        public ICollection<CandidateFollowCompany> CandidateFollowCompanies { get; } = new List<CandidateFollowCompany>();
        public ICollection<CandidateSaveJob> CandidateSaveJobs { get; } = new List<CandidateSaveJob>();
        public ICollection<CandidateIgnoreJob> CandidateIgnoreJobs { get; } = new List<CandidateIgnoreJob>();
        public ICollection<CandidateViewedJob> CandidateViewedJobs { get; } = new List<CandidateViewedJob>();
        public ICollection<CandidateOrders> CandidateOrderses { get; } = new List<CandidateOrders>();
        public ICollection<CandidateNotification> CandidateNotifications { get; } = new List<CandidateNotification>();

        public virtual ICollection<CompanyReview> CompanyReviews { get; } = new List<CompanyReview>();

        public virtual ICollection<EmployerViewedCV> EmployerViewedCVs { get; } = new List<EmployerViewedCV>();
        public virtual ICollection<CandidateSkill> CandidateSkills { get; } = new List<CandidateSkill>();

        public virtual ICollection<CandidateCV> CandidateCVs { get; } = new List<CandidateCV>();
        public virtual ICollection<CandidateCVPDF> CandidateCVPDFs { get; } = new List<CandidateCVPDF>();
        public virtual ICollection<EmployerWalletHistory> EmployerWalletHistories { get; } = new List<EmployerWalletHistory>();

    }
}
