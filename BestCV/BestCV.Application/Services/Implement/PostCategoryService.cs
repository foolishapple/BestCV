using AutoMapper;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Models.PostCategory;
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
    public class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryRepository postCategoryRepository;
        private readonly ILogger<IPostCategoryService> logger;
        private readonly IMapper mapper;
        public PostCategoryService(IPostCategoryRepository postCategoryRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.postCategoryRepository = postCategoryRepository;
            this.logger = loggerFactory.CreateLogger<IPostCategoryService>();
        }


        public async Task<BestCVResponse> CreateAsync(InsertPostCategoryDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await postCategoryRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }
			var isColorExist = await postCategoryRepository.IsColorExistAsync(0, obj.Color.Trim());
			if (isColorExist)
			{
				listErrors.Add("Màu đã tồn tại.");

			}
			if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var newObj = mapper.Map<PostCategory>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await postCategoryRepository.CreateAsync(newObj);
            await postCategoryRepository.SaveChangesAsync();
            return BestCVResponse.Success(newObj);

        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertPostCategoryDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await postCategoryRepository.FindByConditionAsync(s=>s.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);

            }
            var temp = mapper.Map<List<PostCategoryDTO>>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await postCategoryRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", id);

            }
            var temp = mapper.Map<PostCategoryDTO>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await postCategoryRepository.SoftDeleteAsync(id);
            if (data)
            {
                await postCategoryRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdatePostCategoryDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await postCategoryRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
			var isColorExist = await postCategoryRepository.IsColorExistAsync(obj.Id, obj.Color.Trim());
			if (isColorExist)
			{
				listErrors.Add("Màu đã tồn tại.");

			}
			if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var data = await postCategoryRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await postCategoryRepository.UpdateAsync(updateObj);
            await postCategoryRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdatePostCategoryDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
