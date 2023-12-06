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
    public class CandidateCertificate : EntityCommon<long>
    {
        /// <summary>
        /// Cấp bởi
        /// </summary>
        public string IssueBy { get; set; }
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }
        /// <summary>
        /// Khoảng thời gian
        /// </summary>
        public string TimePeriod { get; set; }
        public virtual Candidate Candidate { get; }
    }
}
