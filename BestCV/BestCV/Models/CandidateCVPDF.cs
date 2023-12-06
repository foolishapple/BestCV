using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateCVPDF : EntityBase<long>
    {
        /// <summary>
        /// Mã CV
        /// </summary>
        public long CandidateId { get; set; }

        /// <summary>
        /// Đường dẫn của file CV PDF trên Server
        /// </summary>
        public string Url { get; set; } = null!;
        public int CandidateCVPDFTypeId { get; set; }
        public virtual Candidate Candidate { get; } = null!;
        public virtual CandidateCVPDFType CandidateCVPDFType { get; } = null!;
        public virtual ICollection<CandidateApplyJob> CandidateApplyJobs { get; } = new List<CandidateApplyJob>();
    }
}
