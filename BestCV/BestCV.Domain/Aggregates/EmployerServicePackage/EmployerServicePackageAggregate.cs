using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerServicePackage
{
    public class EmployerServicePackageAggregate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedTime { get; set; }
        public int ServicePackageGroupId { get; set; }
        public int ServicePackageTypeId { get; set; }
        public string ServicePackageGroupName { get; set; }
        public string ServicePackageTypeName { get; set; }
    }
}
