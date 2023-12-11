using AutoMapper;
using BestCV.Application.Models.PostStatus;
using BestCV.Application.Models.PostType;
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
    public class PostTypeService : IPostTypeService
    {
        private readonly IPostTypeRepository postTypeRepository;
        private readonly ILogger<IPostTypeService> logger;
        private readonly IMapper mapper;
        public PostTypeService(IPostTypeRepository postTypeRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = loggerFactory.CreateLogger<PostTypeService>();
            this.postTypeRepository = postTypeRepository;
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: add post type
        /// </summary>
        /// <param name="obj">InsertPostTypeDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertPostTypeDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await postTypeRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }
            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<PostType>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await postTypeRepository.CreateAsync(newObj);
            await postTypeRepository.SaveChangesAsync();
            return DionResponse.Success(newObj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertPostTypeDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get list post type
        /// </summary>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await postTypeRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<PostTypeDTO>>(data);
            return DionResponse.Success(temp);
        }
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get post type by id
        /// </summary>
        /// <param name="id">PostTypeId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await postTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<PostTypeDTO>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: soft delte post type by id
        /// </summary>
        /// <param name="id">PostTypeId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await postTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                await postTypeRepository.SaveChangesAsync();
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
        /// Description: update post type
        /// </summary>
        /// <param name="obj">UpdatePostTypeDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdatePostTypeDTO obj)
        {
            var listErrors = new List<string>();    
            var isNameExist = await postTypeRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var data = await postTypeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu.", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await postTypeRepository.UpdateAsync(updateObj);
            await postTypeRepository.SaveChangesAsync();
            return DionResponse.Success(obj);

        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdatePostTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
