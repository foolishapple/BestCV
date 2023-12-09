using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.CompanyFieldOfActivity;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
	public interface ICompanyFieldOfActivityRepository : IRepositoryBaseAsync<CompanyFieldOfActivity, int, JobiContext>
	{

        /// Description : Lấy danh sách lĩnh vực hoạt động của công ty theo companyid
        /// </summary>
        /// <param name="companyId">CompanyId</param>
        /// <returns>object</returns>
        public Task<List<CompanyFieldOfActivityAggregates>> GetFieldActivityByCompanyId(int companyId);

        /// Description : Kiểm tra lĩnh vực hoạt động của công ty có tồn tại không
        /// </summary>
        /// <param></param>
        /// <returns>object</returns>
        public Task<bool> IsExisted(int id, int companyId, int fieldOfActivityId);


        /// Description : xóa lĩnh vực hoạt động của công ty
        /// </summary>
        /// <param></param>
        /// <returns>object</returns>
        public Task<bool> SoftDeleteByCompanyIdAsync(int companyId);
    }
}
