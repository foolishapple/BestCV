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

        public async Task<BestCVResponse> CreateAsync(InsertPostStatusDTO obj)
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
                return BestCVResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<PostStatus>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await postStatusRepository.CreateAsync(newObj);
            await postStatusRepository.SaveChangesAsync();
            return BestCVResponse.Success(newObj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertPostStatusDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await postStatusRepository.FindByConditionAsync(s=>s.Active);
            if (data==null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<PostStatusDTO>>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await postStatusRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<PostStatusDTO>(data);
            return BestCVResponse.Success(temp);
        }

  
        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await postStatusRepository.SoftDeleteAsync(id);
            if (data)
            {
                await postStatusRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);
            }
            return BestCVResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdatePostStatusDTO obj)
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
                return BestCVResponse.BadRequest(listErrors);
            }

            var data = await postStatusRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await postStatusRepository.UpdateAsync(updateObj);
            await postStatusRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdatePostStatusDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
