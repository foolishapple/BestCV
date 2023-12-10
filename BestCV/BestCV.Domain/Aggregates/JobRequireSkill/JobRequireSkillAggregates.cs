using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.JobRequireSkill
{
    public class JobRequireSkillAggregates
    {
        public long Id { get; set; }
        public int SkillId { get; set; }
        public bool IsAdded { get; set; }
        public bool IsDeleted { get; set; }
    }
}
