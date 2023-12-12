using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateSuggestionJobPosition : EntityBase<long>
    {
        /// <summary>
        /// Mã ứng viên 
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        ///  Mã vị trí công việc
        /// </summary>
        public int JobPositionId { get; set; }

        public virtual Candidate Candidate { get;}
        public virtual JobPosition JobPosition { get; }
    }
}
