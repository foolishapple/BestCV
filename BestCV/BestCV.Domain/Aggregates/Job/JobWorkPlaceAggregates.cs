using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Job
{
    public class JobWorkPlaceAggregates
    {
        public long Id { get; set; }
        public long JobRequireCityId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public long JobRequireDistrictId { get; set; }
        public int? DistrictId { get; set; }
        public string? AddressDetail { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdded { get; set; }
    }
}
