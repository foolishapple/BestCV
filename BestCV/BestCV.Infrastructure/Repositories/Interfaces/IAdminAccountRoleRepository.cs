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
    public interface IAdminAccountRoleRepository : IRepositoryBaseAsync<AdminAccountRole,long,JobiContext>
    {
       
        /// Description: check tài khoản có phải admin không ?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsAdminRole(long id);
    }
}
