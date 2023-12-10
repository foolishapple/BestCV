using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class CountJobCondition
    {
        public long[] EmployerIds { get; set; } = new long[] { };
        public int[] JobStatusIds { get; set; } = new int[] { };
    }
}
