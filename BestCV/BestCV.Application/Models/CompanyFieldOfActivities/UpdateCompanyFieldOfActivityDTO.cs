using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CompanyFieldOfActivities
{
	public class UpdateCompanyFieldOfActivityDTO : InsertCompanyFieldOfActivityDTO
	{
		/// <summary>
		/// Mã
		/// </summary>
        public int Id { get; set; }
    }
}
