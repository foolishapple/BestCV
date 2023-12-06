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
    public interface IJobTypeRepository :IRepositoryBaseAsync<JobType,int,JobiContext>
    {
        /// <summary>
        /// Author: DucNN
        /// CreatedTime : 27/07/2023
        /// Description : check name exist
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(string name, int id);
    }
}
