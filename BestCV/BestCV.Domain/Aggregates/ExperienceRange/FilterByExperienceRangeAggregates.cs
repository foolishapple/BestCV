using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.ExperienceRange
{
    public class FilterByExperienceRangeAggregates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountJob { get; set; }
    }
}
