using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Coupon
{
    public class CouponAggregates : EntityBase<int>
    {
        public int CouponTypeId { get; set; }
        public string CouponTypeName { get; set; }
        public string Code { get; set; }
        public int EfficiencyTime { get; set; }

    }
}
