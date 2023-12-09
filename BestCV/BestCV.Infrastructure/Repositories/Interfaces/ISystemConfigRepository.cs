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
    public interface ISystemConfigRepository : IRepositoryBaseAsync<SystemConfig,int,JobiContext>
    {
        /// <summary>
        /// Description: Check system config key is existed
        /// </summary>
        /// <param name="key">system config key</param>
        /// <param name="id">system config id</param>
        /// <returns></returns>
        Task<bool> KeyIsExisted(string key, int id);
    }
}
