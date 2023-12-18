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

		Task<BestCVResponse> HardDeleteAsync(int id);


		Task<BestCVResponse> GetFieldActivityByCompanyId(int companyId);


		Task<BestCVResponse> DeleteFieldActivityByCompanyId(int companyId);
	}
}

