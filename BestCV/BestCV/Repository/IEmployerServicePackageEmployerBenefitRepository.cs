using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.EmployerServicePackageEmployerBenefit;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerServicePackageEmployerBenefitRepository : IRepositoryBaseAsync<EmployerServicePackageEmployerBenefit, int,JobiContext>
    {
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 10/08/2023
        /// Description : Lấy danh sách quyền lợi với mã gói dịch vụ
        /// </summary>
        /// <param name="id">Mã gói dịch vụ (EmployerServicePackageId)</param>
        /// <returns></returns>
        Task<List<EmployerServicePackageEmployerBenefitAggregates>> GetByEmployerServicePackageIdAsync(int id);

        /// <summary>
        /// Author : ThanhNd
        /// CreatedtIme : 10/08/2023
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employerPackageId"></param>
        /// <param name="benefitId"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(int id, int employerPackageId, int benefitId);

        Task<bool> UpdateHasBenefitAsync(int id);
    }
}
