using BestCV.Application.Models.CompanyFieldOfActivities;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
	public interface ICompanyFieldOfActivityService : IServiceQueryBase<int, InsertCompanyFieldOfActivityDTO, UpdateCompanyFieldOfActivityDTO, CompanyFieldOfActivityDTO>
	{
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Hard delete Company Field Of Activity
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<DionResponse> HardDeleteAsync(int id);

		/// <summary>
		/// Author: HuyDQ
		/// Created: 14/08/2023
		/// Description: get list Company Field Of Activity by company id
		/// </summary>
		/// <param name="companyId"></param>
		/// <returns></returns>
		Task<DionResponse> GetFieldActivityByCompanyId(int companyId);

		/// <summary>
		/// Author: HuyDQ
		/// Created: 14/08/2023
		/// Description: delete list Company Field Of Activity by company id
		/// </summary>
		/// <param name="companyId"></param>
		/// <returns></returns>
		Task<DionResponse> DeleteFieldActivityByCompanyId(int companyId);
	}
}

