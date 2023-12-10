using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.JobRequireCity
{
    public class JobRequireCityAggregates
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
