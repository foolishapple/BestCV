using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class SkillLevel : EntityCommon<int>
    {
        public virtual ICollection<CandidateSkill> CandidateSkills { get; } = new List<CandidateSkill>();
    }
}
