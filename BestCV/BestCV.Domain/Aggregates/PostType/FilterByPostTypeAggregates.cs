using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.PostType
{
    public class FilterByPostTypeAggregates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountPost { get; set; }
    }
}
