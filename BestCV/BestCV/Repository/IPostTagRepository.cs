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
    public interface IPostTagRepository : IRepositoryBaseAsync<PostTag, int, JobiContext>
    {
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 02/08/2023
        /// Description: list tag id by post id
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>List<int></returns>
        Task<List<int>> ListTagIdByPostId(int postId);

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 02/08/2023
        /// Description: list post tag id by post id
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>List<PostTag></returns>
        Task<List<PostTag>> ListByPostId(int postId);
    }
}
