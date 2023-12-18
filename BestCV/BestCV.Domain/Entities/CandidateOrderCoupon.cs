using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateOrderCoupon : EntityBase<long>
    {
        /// <summary>
        /// Mã đơn hàng ứng viên 
        /// </summary>
        public long CandidateOrderId { get; set; }
        /// <summary>
        /// Mã phiếu giảm giá
        /// </summary>
        public int CouponId { get; set; }

        public virtual CandidateOrders CandidateOrders { get; } 
        public virtual Coupon Coupon { get; }
    }
}
