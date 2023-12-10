using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class SearchJobWithServiceParameters
    {
        public long? CandidateId { get; set; }
        public string OrderCriteria { get; set; } = "Id";
        public bool OrderAscendingDirection { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }
}
