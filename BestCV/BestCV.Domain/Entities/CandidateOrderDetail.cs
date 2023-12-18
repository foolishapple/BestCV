using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateOrderDetail :EntityBase<long>
    {
        /// <summary>
        /// Mã đơn hàng ứng viên
        /// </summary>
        public long CandidateOrderId { get; set; }
        /// <summary>
        /// Mã cấp độ ứng viên
        /// </summary>
        public int CandidateLevelId { get; set; }
        /// <summary>
        ///  Số lượng
        /// </summary>
        public int Quantity { get; set;}
        /// <summary>
        /// Giá
        /// </summary>
        public int Price { get; set;}
        /// <summary>
        /// Giảm giá
        /// </summary>
        public int DiscountPrice { get; set; } 
        /// <summary>
        /// Giá cuối 
        /// </summary>
        public int FinalPrice { get; set; }

        public virtual CandidateOrders CandidateOrders { get;  } 
        public virtual CandidateLevel CandidateLevel { get; } 
    }
}
