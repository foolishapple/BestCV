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
    public interface IAdminAccountRoleRepository : IRepositoryBaseAsync<AdminAccountRole,long,JobiContext>
    {
        /// <summary>
        /// Author : DucNN
        /// CreatedTime : 11/8/2023
        /// Description: check tài khoản có phải admin không ?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsAdminRole(long id);
    }
}
