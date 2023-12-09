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
    public interface IEmployerActivityLogTypeRepository : IRepositoryBaseAsync<EmployerActivityLogType, int, JobiContext>
    {

        /// Description: Check employer activity log type name is existed
        /// </summary>
        /// <param name="name">employer activity log type name</param>
        /// <param name="id">employer activity log type id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
    }
}
