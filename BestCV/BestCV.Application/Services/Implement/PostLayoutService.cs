using AutoMapper;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Models.PostLayout;
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
    public class PostLayoutService : IPostLayoutService
    {
        private readonly IPostLayoutRepository postLayoutRepository;
        private readonly ILogger<IPostLayoutService> logger;
        private readonly IMapper mapper;
        public PostLayoutService(IPostLayoutRepository postLayoutRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = loggerFactory.CreateLogger<PostLayoutService>();
            this.postLayoutRepository = postLayoutRepository;
        }


        public async Task<BestCVResponse> CreateAsync(InsertPostLayoutDTO obj)
        {
            var listErrors = new List<string>();    
            var isNameExist = await postLayoutRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }
            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var newObj = mapper.Map<PostLayout>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await postLayoutRepository.CreateAsync(newObj);
            await postLayoutRepository.SaveChangesAsync();
            return BestCVResponse.Success(newObj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertPostLayoutDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await postLayoutRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<PostLayoutDTO>>(data);
            return BestCVResponse.Success(temp);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await postLayoutRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<PostLayoutDTO>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {

            var data = await postLayoutRepository.SoftDeleteAsync(id);
            if (data)
            {
                await postLayoutRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdatePostLayoutDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await postLayoutRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var data = await postLayoutRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await postLayoutRepository.UpdateAsync(updateObj);

            await postLayoutRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdatePostLayoutDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
