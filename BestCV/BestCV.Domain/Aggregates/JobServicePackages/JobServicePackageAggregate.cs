using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.JobServicePackages
{
    public class JobServicePackageAggregate
    {
        public long Id { get; set; }
        public string ServicePackageName { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public int? ExpireTime { get; set; }
        public DateTime? ApplyEndDate { get
            {
                return ExpireTime == null ? null : CreatedTime.AddDays((double)ExpireTime);
            }
        }
        public int Quantity { get; set; }
    }
}
