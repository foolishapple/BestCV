using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CVTemplate
{
    public class CVTemplateListAggregate 
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Photo { get; set; }

        public int OrderSort { get; set; }

        public string? Version { get; set; }
    }
}
