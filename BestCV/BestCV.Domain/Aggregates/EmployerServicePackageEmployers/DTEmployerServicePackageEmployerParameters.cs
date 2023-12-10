using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.EmployerServicePackageEmployers
{
    public class DTEmployerServicePackageEmployerParameters : DTParameters
    {
        public long? EmployerId { get; set; }
        public int[] EmployerServicePackageTypeId { get; set; } = new int[] { };
    }
}
