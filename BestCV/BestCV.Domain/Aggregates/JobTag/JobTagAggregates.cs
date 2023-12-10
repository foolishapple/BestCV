using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.JobTag
{
    public class JobTagAggregates
    {
        public long Id { get; set; }
        public int TagId { get; set; }
        public bool IsAdded { get; set; }
        public bool IsDeleted { get; set; }
    }
}
