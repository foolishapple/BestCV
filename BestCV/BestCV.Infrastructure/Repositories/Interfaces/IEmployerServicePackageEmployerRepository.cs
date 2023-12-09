using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.EmployerServicePackageEmployers;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerServicePackageEmployerRepository : IRepositoryBaseAsync<EmployerServicePackageEmployer,long,JobiContext>
    {
        /// <summary>
        /// Description: Check service package added in order
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        Task<bool> CheckServiceAdded(long id);
        /// <summary>
        /// Description: Paging group by parameters
        /// </summary>
        /// <param name="parameters">paging parameters</param>
        /// <returns></returns>
        Task<List<EmployerServicePackageEmployerGroupAggregate>> GroupByParameters(DTEmployerServicePackageEmployerParameters parameters);
        /// <summary>
        /// Description: find employer service package id 
        /// </summary>
        /// <param name="employerId">mã nhà tuyển dụng</param>
        /// <param name="servicePackageId">mã gói dịch vụ</param>
        /// <returns></returns>
        Task<List<EmployerServicePackageEmployer>> FindByParams(long employerId, long servicePackageId,long orderId);
    }
}
