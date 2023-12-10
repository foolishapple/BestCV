using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.SalaryRange
{
    public class FilterBySalaryRangeAggragates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountJob { get; set; }
    }
}
