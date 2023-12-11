using BestCV.Application.Models.Post;
using BestCV.Application.Models.PostCategory;
using BestCV.Application.Models.Tag;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IPostService : IServiceQueryBase<int, InsertPostDTO, UpdatePostDTO, PostDTO>
    {

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
        /// CreatedAt: 28/07/2023
        /// Description: approve post
        /// </summary>
        /// <param name="obj">ApprovePostDTO</param>
        /// <returns>DionResponse</returns>
        public Task<DionResponse> UpdateApproveStatusPostAsync(ApprovePostDTO obj);

        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 21/08/2023
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<DionResponse> ListPostHomePageAsync(PostParameters parameter);

        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 21/08/2023
        /// </summary>
        /// <returns></returns>
        Task<DionResponse> LoadDataFilterPostHomePageAsync();
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 11/09/2023
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<PostCategoryDTO> GetCategoryAsync(int categoryId);
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 11/09/2023
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        Task<TagDTO> GetTagAsync(int tagId);
    }
}
