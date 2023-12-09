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
    public interface IPostTypeRepository : IRepositoryBaseAsync<PostType, int, JobiContext>
    {
        /// <summary>
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">PostTypeId</param>
        /// <param name="name">PostTypeName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);
    }
}
