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
    public interface IAccountStatusRepository : IRepositoryBaseAsync<AccountStatus, int, JobiContext>
    {
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 01/08/2023
        /// </summary>
        /// <param name="name">accountStatusName</param>
        /// <param name="id">accountStatusId</param>
        /// <returns>True if exist, opposite false</returns>
        Task<bool> IsAccountStatusExistAsync(string name, int id);
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 01/08/2023
        /// </summary>
        /// <param name="name">accountStatusColor</param>
        /// <param name="id">accountStatusId</param>
        /// <returns>True if exist, opposite false</returns>
        Task<bool> IsColorExistAsync(string color, int id);

    }
}
