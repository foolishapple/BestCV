using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CandidateCVs
{
    public class CandidateCVAggregate
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public long? CVTemplateId { get; set; }
        
        public string? CVTemplateName { get; set; }

        public string? CVTemplatePhoto { get; set; }

        public DateTime ModifiedTime { get; set; }
    }
}
