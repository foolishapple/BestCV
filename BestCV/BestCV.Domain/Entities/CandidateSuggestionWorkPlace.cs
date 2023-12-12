using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateSuggestionWorkPlace : EntityBase<int> 
    {
        /// <summary>
        /// Mã ứng viên 
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Mã nơi làm việc
        /// </summary>
        public int WorkPlaceId { get; set; }

        public virtual Candidate Candidate { get;  } 

        public virtual WorkPlace WorkPlace { get; }
    }
}
