using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerServicePackage
{
    public class EmployerServicePackageDTO : EntityCommon<int>
    {
        public int Price { get; set; }

        public int DiscountPrice { get; set; }

        public DateTime? DiscountEndDate { get; set; }
        public int ExpiryTime { get; set; }
        public int ServicePackageGroupId { get; set; }
        public int ServicePackageTypeId { get; set; }
    }
}
