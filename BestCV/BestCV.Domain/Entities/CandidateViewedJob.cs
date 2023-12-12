using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateViewedJob:EntityBase<long>
    {
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã học viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Người tạo : Chung
        /// Mô tả : Mã công việc
        /// </summary>
        public long JobId { get; set; }

        public virtual Candidate Candidate { get; } = null!;
        public virtual Job Job { get;  } = null!;
    }
}
