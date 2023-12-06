using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.JobServicePackages;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface  IJobServicePackageRepository : IRepositoryBaseAsync<JobServicePackage,long,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 18/09/2023
        /// Description: List job service package aggregate
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<List<JobServicePackageAggregate>> ListAggregate(long id);
    }
}
