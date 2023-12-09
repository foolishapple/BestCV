using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Permissions;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IPermissionRepository : IRepositoryBaseAsync<Permission,int, JobiContext>
    {
        /// <summary>
        /// Description: Check name of role is existed
        /// </summary>
        /// <param name="name">permission name</param>
        /// <param name="id">permission id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        /// <summary>
        /// Description: Check permission CODE is existed
        /// </summary>
        /// <param name="code">permission code</param>
        /// <param name="id">permission id</param>
        /// <returns></returns>
        Task<bool> CODEIsExisted(string code, int id);
        /// <summary>
        /// Description: Get permission detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PermissionAggregate> Detail(int id);
    }
}
