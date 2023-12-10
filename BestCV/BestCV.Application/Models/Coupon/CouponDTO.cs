using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Coupon
{
	public class CouponDTO
	{
		public int Id { get; set; }
		public int CouponTypeId { get; set; }
		public string CouponTypeName { get; set; }
		public string Code { get; set; }
		public bool Active { get; set; }
		public int EfficiencyTime { get; set; }
		public DateTime CreatedTime { get; set; }
	}
}
