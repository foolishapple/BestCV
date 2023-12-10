using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.ServicePackageBenefit
{
    public class ServicePackageBenefitAggregate
    {
        public int Id { get; set; }
        public int EmployerServiePackageId { get; set; }
        public string EmployerServiePackageName { get; set; }
        public int BenefitId { get; set; }
        public string BenefitName { get; set; }
        public int? Value { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Active { get; set; }
    }
}
