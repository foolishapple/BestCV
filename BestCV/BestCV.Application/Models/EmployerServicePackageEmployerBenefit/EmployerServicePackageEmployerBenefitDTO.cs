using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerServicePackageEmployerBenefit
{
    public class EmployerServicePackageEmployerBenefitDTO : EntityBase<int>
    {
        public int EmployerServicePackageId { get; set; }
        public int EmployerBenefitId { get; set; }
        public string? Value { get; set; }
        public bool HasBenefit { get; set; }
    }
}
