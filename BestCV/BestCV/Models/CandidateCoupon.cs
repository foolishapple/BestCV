using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateCoupon : EntityBase<long>
    {
        /// <summary>
        /// Mã ứng viên
        /// </summary>
        public long CandidateId { get; set; }

        /// <summary>
        /// Mã phiếu giảm giá
        /// </summary>
        public int CouponId { get; set; }

        public virtual Candidate Candidate { get; }

        public virtual Coupon Coupon {get;}
    }
}
