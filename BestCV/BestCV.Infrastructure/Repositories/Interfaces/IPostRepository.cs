using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.Post;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepositoryBaseAsync<Post, int, JobiContext>
    {
		/// <summary>
		/// Description: check name is exist in same category
		/// </summary>
		/// <param name="id">PostId</param>
		/// <param name="postCategoryId">postCategoryId</param>
		/// <param name="name">PostName</param>
		/// <returns>bool</returns>
		Task<bool> IsNameExistInSameCategoryAsync(int id, int postCategoryId,string name);

        /// <summary>
        /// Description: get list PostAggregates
        /// </summary>
        /// <param name="parameters">DataTableModel DTParamenters</param>
        /// <returns>Object</returns>
        public Task<Object> ListPostAggregatesAsync(DTParameters parameters);

        /// <summary>
        /// Description: Detail PostAggregates
        /// </summary>
        /// <param name="id">PostId</param>
        /// <returns>PostAggregates</returns>
        public Task<PostAggregates> DetailPostByIdAsync(int id);

        /// <summary>
        /// Description: approve post
        /// </summary>
        /// <param name="obj">Post</param>
        /// <returns>bool</returns>
        public Task<bool> UpdateApproveStatusPostAsync(Post obj);
        /// <summary>
        /// Description : Lấy danh sách 4 bài viết trên trang chủ
        /// </summary>
        /// <returns></returns>
        Task<List<PostAggregates>> ListPostShowonHomepage();

        /// <summary>
        /// Description: Tìm kiếm bài viết trang chủ (server side)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<PagingData<List<PostAggregates>>> ListPostHomePageAsync(PostParameters parameter);
        
        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<object> LoadDataFilterPostHomePageAsync();
    }
}
