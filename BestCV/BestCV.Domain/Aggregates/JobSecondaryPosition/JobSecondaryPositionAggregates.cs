using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.JobSecondaryPosition
{
    public class JobSecondaryCategoryAggregates
    {
        public long Id { get; set; }
        public int JobCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public bool IsAdded { get; set; }
        public bool IsDeleted { get; set; }
    }
}
