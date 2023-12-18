using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Nhóm gói dịch vụ
    /// </summary>
    public class ServicePackageGroup : EntityCommon<int>
    {
        public virtual ICollection<EmployerServicePackage> EmployerServicePackages { get; } = new List<EmployerServicePackage>();
    }
}
