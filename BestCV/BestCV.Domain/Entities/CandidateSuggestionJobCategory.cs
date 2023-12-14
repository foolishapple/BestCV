using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateSuggestionJobCategory : EntityBase<long>
    {
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Mã ngành nghề
        /// </summary>
        public int JobCategoryId { get;set; }

        public virtual Candidate Candidate { get;}
        public virtual JobCategory JobCategory { get;}
    }
}
