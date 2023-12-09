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
    public interface IJobStatusRepository : IRepositoryBaseAsync<JobStatus, int,JobiContext>
    {
        /// <summary>
        /// Description: Check name job status is existed
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsNameExistAsync(string name,int id);
        Task<bool> ColorIsExit(string color, int id);
    }
}
