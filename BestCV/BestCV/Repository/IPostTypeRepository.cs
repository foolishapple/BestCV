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
    public interface IPostTypeRepository : IRepositoryBaseAsync<PostType, int, JobiContext>
    {
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">PostTypeId</param>
        /// <param name="name">PostTypeName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);
    }
}
