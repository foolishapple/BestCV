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
    public interface IPostStatusRepository : IRepositoryBaseAsync<PostStatus, int, JobiContext>
    {
        /// <summary>
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">PostStatusId</param>
        /// <param name="name">PostStatusName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);

        /// <summary>
        /// Description: check color is exist
        /// </summary>
        /// <param name="id">PostStatusId</param>
        /// <param name="color">PostStatusColor</param>
        /// <returns>bool</returns>
        Task<bool> IsColorExistAsync(int id, string color);
    }
}
