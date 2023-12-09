using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.JobServicePackages;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface  IJobServicePackageRepository : IRepositoryBaseAsync<JobServicePackage,long,JobiContext>
    {
        /// <summary>
        /// Description: List job service package aggregate
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<List<JobServicePackageAggregate>> ListAggregate(long id);
    }
}
