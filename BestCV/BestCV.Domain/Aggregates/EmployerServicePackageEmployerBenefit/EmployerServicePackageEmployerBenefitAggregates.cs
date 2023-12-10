using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerServicePackageEmployerBenefit
{
    public class EmployerServicePackageEmployerBenefitAggregates : EntityBase<int>
    {
        public int EmployerServicePackageId { get; set; }
        public string EmployerServicePackageName { get; set; } = null!;
        public int EmployerBenefitId { get; set; }
        public string EmployerBenefitName { get; set; } = null!;
        public string? Value { get; set; }
        public bool HasBenefit { get; set; }
    }
}
