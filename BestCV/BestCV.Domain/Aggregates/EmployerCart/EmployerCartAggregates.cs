using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerCart
{
    public class EmployerCartAggregates
    {
        public long Id { get; set; }
        public long EmployerId { get; set; }
        public string EmployerName { get; set; }
        public int EmployerServicePackageId { get; set; }
        public string? EmployerServicePackageName { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Price { get; set; }
        public string? EmployerServicePackgeDescription { get; set; }

    }
}
