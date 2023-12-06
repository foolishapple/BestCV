using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.CompanyFieldOfActivity;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
	public interface ICompanyFieldOfActivityRepository : IRepositoryBaseAsync<CompanyFieldOfActivity, int, JobiContext>
	{
        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 11/08/2023
        /// Description : Lấy danh sách lĩnh vực hoạt động của công ty theo companyid
        /// </summary>
        /// <param name="companyId">CompanyId</param>
        /// <returns>object</returns>
        public Task<List<CompanyFieldOfActivityAggregates>> GetFieldActivityByCompanyId(int companyId);

        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 11/08/2023
        /// Description : Kiểm tra lĩnh vực hoạt động của công ty có tồn tại không
        /// </summary>
        /// <param></param>
        /// <returns>object</returns>
        public Task<bool> IsExisted(int id, int companyId, int fieldOfActivityId);

        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 14/08/2023
        /// Description : xóa lĩnh vực hoạt động của công ty
        /// </summary>
        /// <param></param>
        /// <returns>object</returns>
        public Task<bool> SoftDeleteByCompanyIdAsync(int companyId);
    }
}
