using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class JobSkill : EntityCommon<int>
    {
        public virtual ICollection<JobRequireJobSkill> JobRequireJobSkills { get; } = new List<JobRequireJobSkill>();   
        public ICollection<CandidateSuggestionJobSkill> CandidateSuggestionJobSkills { get; } = new List<CandidateSuggestionJobSkill>();
    }
}
