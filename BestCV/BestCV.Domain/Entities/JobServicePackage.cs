using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Gói dịch vụ của Tin tuyển dụng
    /// </summary>
    public class JobServicePackage : EntityBase<long>, IEntityBase<long>
    {
        /// <summary>
        /// Mổ tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã tin tuyển dụng
        /// </summary>
        public long JobId { get; set; }
        public int? ExpireTime { get; set; }
        public int? Value { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }
        public int EmployerServicePackageId { get; set; }
        public virtual Job Job { get; } = null!;
        public virtual EmployerServicePackage EmployerServicePackage { get; } = null!;
    }
}
