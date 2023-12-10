using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateSkill
{
    public  class CandidateSkillDTO : EntityCommon<long>
    {
        public bool IsDeleted { get; set; } = false;
        public bool IsAdded { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
        public int SkillLevelId { get; set; }
        public int SkillId { get; set; }

    }
}
