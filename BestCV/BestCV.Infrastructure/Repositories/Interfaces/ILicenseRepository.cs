using BestCV.Core.Repositories;
using BestCV.Infrastructure.Persistence;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestCV.Domain.Aggregates.License;
using BestCV.Core.Utilities;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ILicenseRepository : IRepositoryBaseAsync<License, long, JobiContext>
    {
        public Task<List<LicenseAggregates>> GetListByCompanyId(int companyId);

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
        /// <param name="obj">Post</param>
        /// <returns>bool</returns>
        public Task<bool> UpdateApproveStatusLicenseAsync(License obj);
    }
}
