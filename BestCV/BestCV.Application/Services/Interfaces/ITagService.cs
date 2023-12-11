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
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: add tag for post
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
		Task<DionResponse> AddTagForPostAsync(InsertTagDTO obj);

        /// <summary>
        /// Author: truongthieuhuyen
        /// CreatedAt: 18/08/2023
        /// Description: add tag for job
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
		Task<DionResponse> AddTagForJobAsync(InsertTagDTO obj);

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: list tag
        /// </summary>
        /// <param name="obj">TagForSelect2Aggregates</param>
        /// <returns>object</returns>
        Task<Object> ListSelectTagAsync(TagForSelect2Aggregates obj);


        /// <summary>
        /// author: truongthieuhuyen
        /// created: 18.08.2023
        /// description: list tag type job
        /// </summary>
        /// <returns></returns>
        Task<DionResponse> ListTagTypeJob();

        /// <summary>
        /// Author: TrungHieuTr
        /// created: 30.08.2023
        /// description: list tag type post
        /// </summary>
        /// <returns></returns>
        Task<DionResponse> ListTagTypePost();

        /// <summary>
        /// Authoor: TrungHieuTr
        /// created: 13/09/2023
        /// description: get list TagAggregates
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<Object> ListTagAggregatesAsync(DTParameters parameters);
    }
}
