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
    public interface IJobTypeRepository :IRepositoryBaseAsync<JobType,int,JobiContext>
    {
        /// <summary>
        /// Description : check name exist
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(string name, int id);
    }
}
