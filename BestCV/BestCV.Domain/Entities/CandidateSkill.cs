using Jobi.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateSkill : EntityCommon<long>
    {
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }

        public int SkillLevelId { get; set; }

        public int SkillId { get; set; }

        public virtual Candidate Candidate { get; }
        public virtual SkillLevel SkillLevel { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
