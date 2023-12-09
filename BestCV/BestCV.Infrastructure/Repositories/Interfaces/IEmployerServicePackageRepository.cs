using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.EmployerServicePackage;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IEmployerServicePackageRepository : IRepositoryBaseAsync<EmployerServicePackage, int, JobiContext>
    {
        /// <summary>
        /// Description : Check is exist
        /// </summary>
        /// <param name="id">EmployerServicePackageId</param>
        /// <param name="name">EmployerServicePackageId</param>
        /// <returns></returns>
        Task<bool> IsEmployerServicePackageRepositoryExist(int id, string name);
        Task<List<EmployerServicePackageAggregate>> GetListAggregate();
    }
}
