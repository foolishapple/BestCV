using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IRefreshJobRepository : IRepositoryBaseAsync<RefreshJob,long,JobiContext>
    {
        /// <summary>
        /// Description: Refresh job by id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<bool> RefreshJob(long id);
    }
}
