using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.JobRequireJobSkill
{
    public class JobRequireJobSkillAggregates
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public int JobSkillId { get; set; }
        public string JobSkillName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
