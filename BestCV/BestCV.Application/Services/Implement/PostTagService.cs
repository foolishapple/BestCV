using AutoMapper;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Models.PostStatus;
using BestCV.Application.Models.PostTag;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class PostTagService : IPostTagService
    {
        private readonly IPostStatusRepository postStatusRepository;
        private readonly ILogger<IPostTagService> logger;
        private readonly IMapper mapper;
        public PostTagService(IPostStatusRepository postStatusRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.postStatusRepository = postStatusRepository;
            this.logger = loggerFactory.CreateLogger<IPostTagService>();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: add post tag
        /// </summary>
        /// <param name="obj">InsertPostTagDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertPostTagDTO obj)
        {

            var newObj = mapper.Map<PostStatus>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await postStatusRepository.CreateAsync(newObj);
            await postStatusRepository.SaveChangesAsync();
            return DionResponse.Success(newObj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertPostTagDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: list post tag
        /// </summary>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await postStatusRepository.FindByConditionAsync(s=>s.Active);
            if (data==null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<PostStatusDTO>>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get post tag by id
        /// </summary>
        /// <param name="id">PostStatusId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await postStatusRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<PostStatusDTO>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: soft delete post tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await postStatusRepository.SoftDeleteAsync(id);
            if (data)
            {
                await postStatusRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }
            return DionResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: update post statsus 
        /// </summary>
        /// <param name="obj">UpdatePostTagDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdatePostTagDTO obj)
        {

            var data = await postStatusRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await postStatusRepository.UpdateAsync(updateObj);
            await postStatusRepository.SaveChangesAsync();
            return DionResponse.Success(obj);

        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdatePostTagDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
