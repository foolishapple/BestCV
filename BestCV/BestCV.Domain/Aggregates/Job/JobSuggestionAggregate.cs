using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class JobSuggestionAggregate
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string CompanyPhoto { get; set; } = null!;
    }
}
