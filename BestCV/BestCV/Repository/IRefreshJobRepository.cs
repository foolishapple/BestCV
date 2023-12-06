using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IRefreshJobRepository : IRepositoryBaseAsync<RefreshJob,long,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 02/10/2023
        /// Description: Refresh job by id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<bool> RefreshJob(long id);
    }
}
