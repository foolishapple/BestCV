using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.EmployerServicePackageEmployers;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerServicePackageEmployerRepository : IRepositoryBaseAsync<EmployerServicePackageEmployer,long,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/09/2023
        /// Description: Check service package added in order
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        Task<bool> CheckServiceAdded(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 17/09/2023
        /// Description: Paging group by parameters
        /// </summary>
        /// <param name="parameters">paging parameters</param>
        /// <returns></returns>
        Task<List<EmployerServicePackageEmployerGroupAggregate>> GroupByParameters(DTEmployerServicePackageEmployerParameters parameters);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 18/09/2023
        /// Description: find employer service package id 
        /// </summary>
        /// <param name="employerId">mã nhà tuyển dụng</param>
        /// <param name="servicePackageId">mã gói dịch vụ</param>
        /// <returns></returns>
        Task<List<EmployerServicePackageEmployer>> FindByParams(long employerId, long servicePackageId,long orderId);
    }
}
