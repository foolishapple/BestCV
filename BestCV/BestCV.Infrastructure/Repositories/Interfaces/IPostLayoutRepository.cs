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
    public interface IPostLayoutRepository : IRepositoryBaseAsync<PostLayout, int, JobiContext>
    {
        /// <summary>
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">PostLayoutId</param>
        /// <param name="name">PostLayoutName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);
    }
}
