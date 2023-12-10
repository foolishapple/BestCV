using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Coupon
{
	public class InsertCouponDTO
	{
		public int CouponTypeId { get; set; }
		public string Code { get; set; }
		public int EfficiencyTime { get; set; }
	}
}
