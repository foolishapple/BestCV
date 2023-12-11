using BestCV.Application.Models.License;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ILicenseService : IServiceQueryBase<long, InsertLicenseDTO, UpdateLicenseDTO, LicenseDTO>
    {
        public Task<DionResponse> GetListByCompanyId(int companyId);

        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 07/09/2023
        /// Description : Lấy danh sách giầy tờ nhà tuyển dụng (dành cho admin)
        /// </summary>
        /// <param name="parameters">DTParameters</param>
        /// <returns>object</returns>
        public Task<object> ListLicenseAggregates(DTParameters parameters);

        /// <summary>
        /// Author: HuyDQ
        /// CreatedAt: 07/09/2023
        /// Description: approve license
        /// </summary>
        /// <param name="obj">ApproveLicenseDTO</param>
        /// <returns>DionResponse</returns>
        public Task<DionResponse> UpdateApproveStatusLicenseAsync(ApproveLicenseDTO obj);
    }
}
