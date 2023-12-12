using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateSuggestionJobSkill : EntityBase<long>
    {
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get;set; }
        /// <summary>
        /// Mã kỹ năng việc làm
        /// </summary>
        public int JobSkillId { get; set; }

        public virtual Candidate Candidate { get;} 
        public virtual JobSkill JobSkill { get;}
    }
}
