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


        public Task<Object> ListPostAggregatesAsync(DTParameters parameters);


        public Task<BestCVResponse> UpdateApproveStatusPostAsync(ApprovePostDTO obj);


        Task<BestCVResponse> ListPostHomePageAsync(PostParameters parameter);

        Task<BestCVResponse> LoadDataFilterPostHomePageAsync();

        Task<PostCategoryDTO> GetCategoryAsync(int categoryId);

        Task<TagDTO> GetTagAsync(int tagId);
    }
}
