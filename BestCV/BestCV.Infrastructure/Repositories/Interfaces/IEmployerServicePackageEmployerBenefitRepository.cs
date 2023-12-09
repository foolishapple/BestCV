using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.EmployerServicePackageEmployerBenefit;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerServicePackageEmployerBenefitRepository : IRepositoryBaseAsync<EmployerServicePackageEmployerBenefit, int,JobiContext>
    {
        /// <summary>
        /// Description : Lấy danh sách quyền lợi với mã gói dịch vụ
        /// </summary>
        /// <param name="id">Mã gói dịch vụ (EmployerServicePackageId)</param>
        /// <returns></returns>
        Task<List<EmployerServicePackageEmployerBenefitAggregates>> GetByEmployerServicePackageIdAsync(int id);

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employerPackageId"></param>
        /// <param name="benefitId"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(int id, int employerPackageId, int benefitId);

        Task<bool> UpdateHasBenefitAsync(int id);
    }
}
