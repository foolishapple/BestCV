using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class RecruitCampainJobAggregate
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long RecruitCampainId { get; set; }
        public bool IsApproved { get; set; }
        public string StatusName { get; set; } = null!;
        public string StatusColor { get; set; } = null!;
        public int TotalCandidateApply { get; set; }
        public int ViewCount { get; set; }
    }
}
