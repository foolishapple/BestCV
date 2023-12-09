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
    public interface IPostTagRepository : IRepositoryBaseAsync<PostTag, int, JobiContext>
    {
        /// <summary>
        /// Description: list tag id by post id
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>List<int></returns>
        Task<List<int>> ListTagIdByPostId(int postId);

        /// <summary>
        /// Description: list post tag id by post id
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>List<PostTag></returns>
        Task<List<PostTag>> ListByPostId(int postId);
    }
}
