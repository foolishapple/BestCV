using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerOrder
{
    public class UpdateEmployerOrderStatusDTO
    {
        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Mã trạng thái đơn hàng
        /// </summary>
        public int OrderStatusId { get; set; }
    }
}
