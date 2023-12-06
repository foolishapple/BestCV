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
    public interface IJobStatusRepository : IRepositoryBaseAsync<JobStatus, int,JobiContext>
    {
        /// <summary>
        /// Author: HuyDQ
        /// Created: 27/07/2003
        /// Description: Check name job status is existed
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsNameExistAsync(string name,int id);
        Task<bool> ColorIsExit(string color, int id);
    }
}
