using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    /// <summary>
    /// Loại gói dịch vụ
    /// </summary>
    public class ServicePackageType :EntityCommon<int>
    {
        public virtual ICollection<EmployerServicePackage> EmployerServicePackages { get; } = new List<EmployerServicePackage>();
    }
}
