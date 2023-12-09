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
    public interface IPostCategoryRepository : IRepositoryBaseAsync<PostCategory, int, JobiContext>
    {
        /// <summary>
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">PostCategoryId</param>
        /// <param name="name">PostCategoryName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);

        /// <summary>
        /// Description: check color is exist
        /// </summary>
        /// <param name="id">PostCategoryId</param>
        /// <param name="color">PostCategoryColor</param>
        /// <returns>bool</returns>
        Task<bool> IsColorExistAsync(int id, string color);
    }
}
