using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CompanyFieldOfActivities
{
	public class InsertCompanyFieldOfActivityDTO
	{
		/// <summary>
		/// Mã công ty
		/// </summary>
		public int CompanyId { get; set; }
		/// <summary>
		/// Lĩnh vực hoạt động
		/// </summary>
		public int FieldOfActivityId { get; set; }
	}
}
