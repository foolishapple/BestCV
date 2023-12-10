using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Candidate
{
    public class CandidateDTParameters : DTParameters
    {
        public string SearchCategory { get; set; } = "";
        public string SearchExperience { get; set; } = "";
        public string SearchPosition { get; set; } = "";
        public string SearchSuggestSkill { get; set; } = "";
        public string SearchCity { get; set; } = "";
        public string SearchEducation { get; set; } = "";
        public string SearchCertificate { get; set; } = "";
        public string SearchSalaryRange { get; set; } = "";
        public string SearchCandidateSkill { get; set; } = "";
        public string SearchCandidateSkillLevel { get; set; } = "";
    }
}
