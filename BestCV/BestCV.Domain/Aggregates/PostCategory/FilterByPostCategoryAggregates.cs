using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.PostCategory
{
    public class FilterByPostCategoryAggregates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountPost { get; set; }
    }
}
