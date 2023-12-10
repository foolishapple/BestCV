using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateApplyJobs
{
    public class CandidateApplyJobDTO : EntityCommon<long>
    {
        public long CandidateId { get; set; }
        public long CandidateCVPDFId { get; set; }
        public string CandidateCvPdfUrl { get; set; }
        public string CandidateName { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public int CandidateApplyJobStatusId { get; set; }
        public string CandidateApplyJobStatusName { get; set; }
        public string CandidateApplyJobStatusColor { get; set; } = null!;
        public int CandidateApplyJobSourceId { get; set; }
        public bool IsEmployerViewed { get; set; }
        public string IsEmployerViewedDisplay { get; set; }
    }
}
