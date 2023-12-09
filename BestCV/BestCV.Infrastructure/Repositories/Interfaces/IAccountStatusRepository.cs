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
    public interface IAccountStatusRepository : IRepositoryBaseAsync<AccountStatus, int, JobiContext>
    {
        /// <param name="name">accountStatusName</param>
        /// <param name="id">accountStatusId</param>
        /// <returns>True if exist, opposite false</returns>
        Task<bool> IsAccountStatusExistAsync(string name, int id);

        /// <param name="name">accountStatusColor</param>
        /// <param name="id">accountStatusId</param>
        /// <returns>True if exist, opposite false</returns>
        Task<bool> IsColorExistAsync(string color, int id);

    }
}
