using BestCV.Application.Models.Post;
using BestCV.Application.Models.PostCategory;
using BestCV.Application.Models.Tag;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ITagService : IServiceQueryBase<int, InsertTagDTO, UpdateTagDTO, TagDTO>
    {

		Task<BestCVResponse> AddTagForPostAsync(InsertTagDTO obj);


		Task<BestCVResponse> AddTagForJobAsync(InsertTagDTO obj);


        Task<Object> ListSelectTagAsync(TagForSelect2Aggregates obj);



        Task<BestCVResponse> ListTagTypeJob();

        Task<BestCVResponse> ListTagTypePost();


        Task<Object> ListTagAggregatesAsync(DTParameters parameters);
    }
}
