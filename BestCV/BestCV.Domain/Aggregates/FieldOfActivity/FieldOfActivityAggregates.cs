using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.FieldOfActivity
{
    public class FieldOfActivityAggregates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountActivity { get; set; }
    }
}
