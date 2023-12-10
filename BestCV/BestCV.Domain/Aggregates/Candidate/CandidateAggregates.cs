using BestCV.Core.Entities.Interfaces;
using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestCV.Domain.Entities;

namespace BestCV.Domain.Aggregates.Candidate
{
    public class CandidateAggregates : EntityBase<long>, IFullTextSearch
    {
        public int CandidateStatusId { get; set; }
        public string CandidateStatusName { get; set; }
        public string CandidateStatusColor { get; set; }
        public int CandidateLevelId { get; set; }
        public string CandidateLevelName { get; set; }
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? GoogleId { get; set; }
        public string? FacebookId { get; set; }
        public string? LinkedinId { get; set; }
        public string? Photo { get; set; }
        public string? CoverPhoto { get; set; }
        public int Gender { get; set; }
        public string? JobPosition { get; set; }
        public string? AddressDetail { get; set; }
        public string? Interests { get; set; }
        public string? Objective { get; set; }
        public string? Info { get; set; }
        public DateTime? DoB { get; set; }
        public string Phone { get; set; }
        public bool IsSubcribeEmailImportantSystemUpdate { get; set; }

        public bool IsSubcribeEmailEmployerViewCV { get; set; }

        public bool IsSubcribeEmailNewFeatureUpdate { get; set; }

        public bool IsSubcribeEmailOtherSystemNotification { get; set; }

        public bool IsSubcribeEmailJobSuggestion { get; set; }

        public bool IsSubcribeEmailEmployerInviteJob { get; set; }

        public bool IsSubcribeEmailServiceIntro { get; set; }

        public bool IsSubcribeEmailProgramEventIntro { get; set; }

        public bool IsSubcribeEmailGiftCoupon { get; set; }

        public bool IsCheckOnJobWatting { get; set; }

        public bool IsCheckJobOffers { get; set; }

        public bool IsCheckViewCV { get; set; }

        public bool IsCheckTopCVReview { get; set; }

        public int SuggestionExperienceRangeId { get; set; }

        public int SuggestionSalaryRangeId { get; set; }

        public int AccessFailedCount { get; set; }

        public bool LockEnabled { get; set; }

        public DateTime? LockEndDate { get; set; }

        public DateTime? CandidateLevelEfficiencyExpiry { get; set; }
        public bool IsActivated { get; set; }
        public string Search { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string References { get; set; }
        public string SuggestionExperienceRangeName { get; set; }
        public string SuggestionSalaryRangeName { get; set; }
        public List<int> ListCandidateSuggestionJobCategory { get; set; } = new List<int>();
        public List<string> ListCandidateSuggestionJobCategoryName { get; set; } = new List<string>();
        public List<int> ListCandidateSuggestionJobPosition { get; set; } = new List<int>();
        public List<string> ListCandidateSuggestionJobPositionName { get; set; } = new List<string>();
        public List<int> ListCandidateSuggestionJobJobSkill { get; set; } = new List<int>();
        public List<string> ListCandidateSuggestionJobJobSkillName { get; set; } = new List<string>();
        public List<int> ListCandidateSuggestionJobWorkplace { get; set; } = new List<int>();
        public List<string> ListCandidateSuggestionJobWorkplaceName { get; set; } = new List<string>();
        public List<string> ListCandidateEducation { get; set; } = new List<string>();
        public List<string> ListCandidateCertificate { get; set; } = new List<string>();
        public List<string> ListCandidateSkill { get; set; } = new List<string>();
        public List<int> ListCandidateSkillLevel { get; set; } = new List<int>();


        public int CountCredit { get; set; }
        public List<CandidateSkill> ListCandidateSkillObject { get; set; } = new List<CandidateSkill>();

        public bool IsUsedCredit { get; set; }
        public bool IsRefundRequest { get; set; }
    }
}
