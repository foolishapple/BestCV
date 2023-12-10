using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerServicePackageEmployers
{
    public class EmployerServicePackageEmployerGroupAggregate
    {
        public long ServicePackageId { get; set; }
        public string ServicePackageName { get; set; } = null!;
        public int ServicePackageTypeId { get; set; }
        public string ServicePackageTypeName { get; set; } =null!;
        public int Quantity { get; set; }
        public long EmployerId { get; set; }
        public long  OrderId { get; set; }
        public int ServicePackageGroupId { get; set; }
        public string ServicePackageGroupName { get; set; } = null!;
    }
}
