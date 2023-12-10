using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerCarts
{
    public class InsertEmployerCartDTO
    {
        /// <summary>
        ///  Mã nhà tuyển dụng có thể được gán khi vào controller
        /// </summary>
        public long? EmployerId { get; set; }
        /// <summary>
        /// Mã dịch vụ
        /// </summary>
        public long EmployerServicePackageId { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
