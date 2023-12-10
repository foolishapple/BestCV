using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class DTJobPagingParameters : DTParameters
    {
        public long? EmployerId { get; set; }
        public int? JobStatusId { get; set; }
    }
}
