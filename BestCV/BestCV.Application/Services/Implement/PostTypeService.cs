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


        public async Task<BestCVResponse> CreateAsync(InsertPostTypeDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await postTypeRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }
            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<PostType>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await postTypeRepository.CreateAsync(newObj);
            await postTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(newObj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertPostTypeDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await postTypeRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<PostTypeDTO>>(data);
            return BestCVResponse.Success(temp);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await postTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<PostTypeDTO>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await postTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                await postTypeRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

 
        public async Task<BestCVResponse> UpdateAsync(UpdatePostTypeDTO obj)
        {
            var listErrors = new List<string>();    
            var isNameExist = await postTypeRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var data = await postTypeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await postTypeRepository.UpdateAsync(updateObj);
            await postTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdatePostTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
