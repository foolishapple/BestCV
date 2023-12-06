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
    public interface ISystemConfigRepository : IRepositoryBaseAsync<SystemConfig,int,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Check system config key is existed
        /// </summary>
        /// <param name="key">system config key</param>
        /// <param name="id">system config id</param>
        /// <returns></returns>
        Task<bool> KeyIsExisted(string key, int id);
    }
}
