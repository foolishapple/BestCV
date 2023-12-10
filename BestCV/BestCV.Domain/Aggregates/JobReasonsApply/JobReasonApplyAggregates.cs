using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.JobReasonsApply
{
    public class JobReasonApplyAggregates
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
