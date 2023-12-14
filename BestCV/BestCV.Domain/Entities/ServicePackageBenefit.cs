using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Quyền lợi gói dịch vụ
    /// </summary>
    public class ServicePackageBenefit : EntityBase<int>
    {
        /// <summary>
        /// Mã gói dịch vụ
        /// </summary>
        public int EmployerServicePackageId { get; set; }
        /// <summary>
        /// Mã quyền lợi
        /// </summary>
        public int BenefitId { get; set; }
        /// <summary>
        /// Giá trị
        /// </summary>
        public int? Value { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        public virtual EmployerServicePackage EmployerServicePackage { get; } = null!;
        public virtual Benefit Benefit { get; } = null!;
    }
}
