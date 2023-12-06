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
    public interface IRoleRepository : IRepositoryBaseAsync<Role,int, JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check name of role is existed
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="id">role id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 26/07/2023
        /// Description: Check role CODE is existed
        /// </summary>
        /// <param name="code">role code</param>
        /// <param name="id">role id</param>
        /// <returns></returns>
        Task<bool> CODEIsExisted(string code, int id);
    }
}
