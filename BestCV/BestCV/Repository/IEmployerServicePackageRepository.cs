using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.EmployerServicePackage;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerServicePackageRepository : IRepositoryBaseAsync<EmployerServicePackage, int, JobiContext>
    {
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime: 03/08/2023
        /// Description : Check is exist
        /// </summary>
        /// <param name="id">EmployerServicePackageId</param>
        /// <param name="name">EmployerServicePackageId</param>
        /// <returns></returns>
        Task<bool> IsEmployerServicePackageRepositoryExist(int id, string name);
        Task<List<EmployerServicePackageAggregate>> GetListAggregate();
    }
}
