using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.JobCategory
{
    public class JobCategoryOnHomePageAggregates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CountJob { get; set; }
        public string Icon { get; set; }
    }
}
