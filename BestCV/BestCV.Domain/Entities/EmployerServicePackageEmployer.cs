using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    /// <summary>
    /// Dịch vụ đã mua của nhà tuyển dụng
    /// </summary>
    public class EmployerServicePackageEmployer : EntityBase<long>
    {
        /// <summary>
        /// Mã chi tiết đơn hàng
        /// </summary>
        public long EmployerOrderDetailId { get; set; }
        public virtual EmployerOrderDetail EmployerOrderDetail { get; } = null!;
    }
}
