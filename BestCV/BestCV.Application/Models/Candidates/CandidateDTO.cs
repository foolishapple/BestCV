using BestCV.Application.Models.CandidateActivites;
using BestCV.Application.Models.CandidateCertificate;
using BestCV.Application.Models.CandidateEducation;
using BestCV.Application.Models.CandidateHonorAward;
using BestCV.Application.Models.CandidateProjects;
using BestCV.Application.Models.CandidateSkill;
using BestCV.Application.Models.CandidateSuggestionJobCategory;
using BestCV.Application.Models.CandidateSuggestionJobPosition;
using BestCV.Application.Models.CandidateSuggestionJobSkill;
using BestCV.Application.Models.CandidateSuggestionWorkPlace;
using BestCV.Application.Models.CandidateWorkExperience;
using BestCV.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Candidates
{
    public class CandidateDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int CandidateStatusId { get; set; }
        public int CandidateLevelId { get; set; }
        public int SuggestionExperienceRangeId { get; set; }
        public int SuggestionSalaryRangeId { get; set; }
        public bool IsActivated { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; } = null!;
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
        public string Phone { get; set; }
        public DateTime CreatedTime { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string References { get; set; }
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

        public List<CandidateWorkExperienceDTO> ListCandidateWorkExperiences { get; set; } = new List<CandidateWorkExperienceDTO>();

        public List<CandidateSkillDTO> ListCandidateSkill { get; set; } = new List<CandidateSkillDTO>();
        public List<CandidateEducationDTO> ListCandidateEducation { get; set; } = new List<CandidateEducationDTO>();
        public List<CandidateHonorAwardDTO> ListCandidateHonorAwards { get; set; } = new List<CandidateHonorAwardDTO>();
        public List<CandidateCertificateDTO> ListCandidateCertificates { get; set; } = new List<CandidateCertificateDTO>();
        public List<CandidateActivitesDTO> ListCandidateActivites { get; set; } = new List<CandidateActivitesDTO>();
        public List<CandidateProjectsDTO> ListCandidateProjects { get; set; } = new List<CandidateProjectsDTO>();
        //Thanh

        public List<CandidateSuggestionJobCategoryDTO> ListCandidateSuggestionJobCategory { get; set;} = new List<CandidateSuggestionJobCategoryDTO>();

        public List<CandidateSuggestionJobPositionDTO> ListCandidateSuggestionJobPosition { get; set; } = new List<CandidateSuggestionJobPositionDTO>();

        public List<CandidateSuggestionJobSkillDTO> ListCandidateSuggestionJobSkill { get; set; } = new List<CandidateSuggestionJobSkillDTO>();
        public List<CandidateSuggestionWorkPlaceDTO> candidateSuggestionWorkPlaces { get; set; } = new List<CandidateSuggestionWorkPlaceDTO>();
    }
}
