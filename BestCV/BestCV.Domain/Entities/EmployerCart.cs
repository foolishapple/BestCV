using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Giỏ hàng nhà tuyển dụng
    /// </summary>
    public class EmployerCart : EntityBase<long>
    {
        /// <summary>
        /// Mã nhà tuyển dụng
        /// </summary>
        public long EmployerId { get; set; }
        /// <summary>
        /// Mã gói dịch vụ
        /// </summary>
        public int EmployerServicePackageId { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        public virtual Employer Employer { get; } = null!;
        public virtual EmployerServicePackage EmployerServicePackage { get; } = null!;
    }
}
