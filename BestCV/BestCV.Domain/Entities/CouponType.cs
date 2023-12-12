using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public partial class CouponType : EntityCommon<int>
    {
        public virtual ICollection<Coupon> Coupon { get; } = new List<Coupon>();

    }
}
