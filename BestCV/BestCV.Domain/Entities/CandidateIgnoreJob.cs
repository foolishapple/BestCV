using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateIgnoreJob : EntityBase<long>
    {
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Mã việc làm
        /// </summary>
        public long JobId { get; set; }

        public virtual Candidate Candidate { get; }
        public virtual Job Job { get; }
    }
}
