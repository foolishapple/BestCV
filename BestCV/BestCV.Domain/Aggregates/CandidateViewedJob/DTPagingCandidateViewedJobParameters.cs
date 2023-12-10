using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CandidateViewedJob
{
    public class DTPagingCandidateViewedJobParameters : DTParameters
    {
        public long? CandidateId { get; set; }
    }
}
