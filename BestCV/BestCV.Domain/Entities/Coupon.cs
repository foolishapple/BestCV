using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class Coupon : EntityBase<int>
    {
        public int CouponTypeId { get; set; }

        public string Code { get; set; }
        /// <summary>
        /// Hiệu lực của mã quà tặng (theo tháng)
        /// </summary>
        public int EfficiencyTime { get; set; }
        public virtual CouponType CouponType { get; set; } = null!;

        //public virtual CouponType CouponType { get; set; } = null!;
        public ICollection<CandidateCoupon> CandidateCoupons { get; } = new List<CandidateCoupon>();
        public ICollection<CandidateOrderCoupon> CandidateOrderCoupons { get;} = new List<CandidateOrderCoupon>();
    }
}
