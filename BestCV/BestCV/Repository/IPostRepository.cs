using Jobi.Core.Entities;
using Jobi.Core.Entities.Interfaces;
using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.Job;
using Jobi.Domain.Aggregates.Post;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepositoryBaseAsync<Post, int, JobiContext>
    {
		/// <summary>
		/// Author: NhatVi
		/// CreatedAt: 28/07/2023
		/// Description: check name is exist in same category
		/// </summary>
		/// <param name="id">PostId</param>
		/// <param name="postCategoryId">postCategoryId</param>
		/// <param name="name">PostName</param>
		/// <returns>bool</returns>
		Task<bool> IsNameExistInSameCategoryAsync(int id, int postCategoryId,string name);

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 28/07/2023
        /// Description: get list PostAggregates
        /// </summary>
        /// <param name="parameters">DataTableModel DTParamenters</param>
        /// <returns>Object</returns>
        public Task<Object> ListPostAggregatesAsync(DTParameters parameters);

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: Detail PostAggregates
        /// </summary>
        /// <param name="id">PostId</param>
        /// <returns>PostAggregates</returns>
        public Task<PostAggregates> DetailPostByIdAsync(int id);

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 02/08/2023
        /// Description: approve post
        /// </summary>
        /// <param name="obj">Post</param>
        /// <returns>bool</returns>
        public Task<bool> UpdateApproveStatusPostAsync(Post obj);
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 21/08/2023
        /// Description : Lấy danh sách 4 bài viết trên trang chủ
        /// </summary>
        /// <returns></returns>
        Task<List<PostAggregates>> ListPostShowonHomepage();

        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 21/08/2023
        /// Description: Tìm kiếm bài viết trang chủ (server side)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<PagingData<List<PostAggregates>>> ListPostHomePageAsync(PostParameters parameter);
        
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 21/08/2023
        /// </summary>
        /// <returns></returns>
        Task<object> LoadDataFilterPostHomePageAsync();
    }
}
