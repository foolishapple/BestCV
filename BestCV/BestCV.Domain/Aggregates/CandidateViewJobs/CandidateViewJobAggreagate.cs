using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CandidateViewJobs
{
    public class CandidateViewedJobAggreagate
    {
        public  long Id { get; set; }

        public long CandidateId { get; set; }

        public long JobId { get; set; }

        public string? CandidatePhoto { get; set; } = null!;

        public string CandidateName { get; set; } = null!;

        public string CandidatePhone { get; set; } = null!;

        public string CandidateEmail { get; set; } = null!;

        public DateTime CreatedTime { get; set; }
    }
}
