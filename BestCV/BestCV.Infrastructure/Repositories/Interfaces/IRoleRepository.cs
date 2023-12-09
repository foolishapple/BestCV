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
    public interface IRoleRepository : IRepositoryBaseAsync<Role,int, JobiContext>
    {
        /// <summary>
        /// Description: Check name of role is existed
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="id">role id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        /// <summary>
        /// Description: Check role CODE is existed
        /// </summary>
        /// <param name="code">role code</param>
        /// <param name="id">role id</param>
        /// <returns></returns>
        Task<bool> CODEIsExisted(string code, int id);
    }
}
