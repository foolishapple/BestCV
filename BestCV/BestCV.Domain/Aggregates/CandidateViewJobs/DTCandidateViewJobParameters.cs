using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CandidateViewJobs
{
    public class DTCandidateViewedJobParameters : DTParameters
    {
        public long? JobId { get; set; }
    }
}
