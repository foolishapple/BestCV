using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.TopFeatureJob
{
    public class Select2Aggregates
    {
        public string SearchString { get; set; }
        public int? PageLimit { get; set; }
    }
}
