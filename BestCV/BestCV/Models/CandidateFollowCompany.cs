using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateFollowCompany : EntityBase<long>
    {
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Mã công ty
        /// </summary>
        public int CompanyId { get; set; }

        public virtual Candidate Candidate { get; }
        public virtual Company Company { get; }
    }
}
