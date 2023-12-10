using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.ServicePackageBenefit
{
    public class InsertServicePackageBenefitDTO
    {
        public int EmployerServicePackageId { get; set; }
        public int BenefitId { get; set; }
        public string? Description { get; set; }
        public int? Value { get; set; }
    }
}
