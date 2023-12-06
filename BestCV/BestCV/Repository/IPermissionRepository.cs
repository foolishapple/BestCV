using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Permissions;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IPermissionRepository : IRepositoryBaseAsync<Permission,int, JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check name of role is existed
        /// </summary>
        /// <param name="name">permission name</param>
        /// <param name="id">permission id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check permission CODE is existed
        /// </summary>
        /// <param name="code">permission code</param>
        /// <param name="id">permission id</param>
        /// <returns></returns>
        Task<bool> CODEIsExisted(string code, int id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 07/08/2023
        /// Description: Get permission detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PermissionAggregate> Detail(int id);
    }
}
