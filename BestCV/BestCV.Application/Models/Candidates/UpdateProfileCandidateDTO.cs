using BestCV.Application.Models.CandidateActivites;
using BestCV.Application.Models.CandidateCertificate;
using BestCV.Application.Models.CandidateEducation;
using BestCV.Application.Models.CandidateHonorAward;
using BestCV.Application.Models.CandidateProjects;
using BestCV.Application.Models.CandidateSkill;
using BestCV.Application.Models.CandidateWorkExperience;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Candidates
{
    public class UpdateProfileCandidateDTO
    {
        public long Id { get; set; } = 0;
        public string FullName { get; set; } = null!;
        public string? Photo { get; set; }
        public string? CoverPhoto { get; set; }
        public string? JobPosition { get; set; }
        public string? AddressDetail { get; set; }
        public string Email { get; set; } = null!;
        public string? Info { get; set; }
        public string Phone { get; set; }
        public string? Interests { get; set; }
        public string? Objective { get; set; }
        public int Gender { get; set; }
        public List<CandidateSkillDTO> CandidateSkills { get; set; } = new List<CandidateSkillDTO>();
        public List<CandidateWorkExperienceDTO> WorkExperiences { get; set; } = new List<CandidateWorkExperienceDTO>();
        public List<CandidateEducationDTO> Educations { get; set; } = new List<CandidateEducationDTO>();
        public List<CandidateHonorAwardDTO> HonorAwards { get; set; } = new List<CandidateHonorAwardDTO>();
        public List<CandidateCertificateDTO> Certificates { get; set; } = new List<CandidateCertificateDTO>();
        public List<CandidateActivitesDTO> CandidateActivites { get; set; } = new List<CandidateActivitesDTO>();

        public List<CandidateProjectsDTO> CandidateProjects { get; set; } = new List<CandidateProjectsDTO>();

    }
}
