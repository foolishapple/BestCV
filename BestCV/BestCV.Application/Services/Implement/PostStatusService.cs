using AutoMapper;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Models.PostStatus;
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
    public class PostStatusService : IPostStatusService
    {
        private readonly IPostStatusRepository postStatusRepository;
        private readonly ILogger<IPostStatusService> logger;
        private readonly IMapper mapper;
        public PostStatusService(IPostStatusRepository postStatusRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.postStatusRepository = postStatusRepository;
            this.logger = loggerFactory.CreateLogger<IPostStatusService>();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: add post status
        /// </summary>
        /// <param name="obj">InsertPostStatusDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertPostStatusDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await postStatusRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            var isColorExist = await postStatusRepository.IsColorExistAsync(0, obj.Color.Trim());
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");

            }

            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<PostStatus>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await postStatusRepository.CreateAsync(newObj);
            await postStatusRepository.SaveChangesAsync();
            return DionResponse.Success(newObj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertPostStatusDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: list post status
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
        /// Description: get post status by id
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
        /// Description: soft delete post status by id
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
        /// <param name="obj">UpdatePostStatusDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdatePostStatusDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await postStatusRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            var isColorExist = await postStatusRepository.IsColorExistAsync(obj.Id, obj.Color.Trim());
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");

            }
            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }

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

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdatePostStatusDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
