using AutoMapper;
using BestCV.Application.Models.JobCategory;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Services;
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
    public class JobCategoryService : IJobCategoryService
    {
        private readonly IJobCategoryRepository jobCategoryRepository;
        private readonly ILogger<IJobCategoryService> logger;
        private readonly IMapper mapper;

        public JobCategoryService(IJobCategoryRepository jobCategoryRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            this.jobCategoryRepository = jobCategoryRepository;
            this.logger = loggerFactory.CreateLogger<IJobCategoryService>();
            this.mapper = _mapper;
        }


        public async Task<BestCVResponse> CreateAsync(InsertJobCategoryDTO obj)
        {
            var listErrors = new List<string>();

            var isNameExist = await jobCategoryRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var jc = mapper.Map<JobCategory>(obj);
            jc.Id = 0;
            jc.Active = true;
            jc.CreatedTime = DateTime.Now;
            jc.Description = !string.IsNullOrEmpty(jc.Description) ? jc.Description.ToEscape() : null;

            await jobCategoryRepository.CreateAsync(jc);
            await jobCategoryRepository.SaveChangesAsync();
            return BestCVResponse.Success(jc);

        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertJobCategoryDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await jobCategoryRepository.FindByConditionAsync(c => c.Active);
            if (data ==null)
            {
                return BestCVResponse.NotFound("Không tìm thấy dữ liệu", data);
            }
            var temp = mapper.Map<List<JobCategoryDTO>>(data);

            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await jobCategoryRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không tìm thấy dữ liệu", data);
            }
            var temp = mapper.Map<JobCategoryDTO>(data);
            return BestCVResponse.Success(temp);
        }

  
        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await jobCategoryRepository.SoftDeleteAsync(id);
            if (data)
            {
                await jobCategoryRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);
            }
            return BestCVResponse.NotFound("Không tìm thấy dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateJobCategoryDTO obj)
        {
            var listErrors = new List<string>();

            var isNameExist = await jobCategoryRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var jc = await jobCategoryRepository.GetByIdAsync(obj.Id);
            if (jc == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateJobCategory = mapper.Map(obj, jc);
            updateJobCategory.Description = !string.IsNullOrEmpty(updateJobCategory.Description) ? updateJobCategory.Description.ToEscape() : null;

            await jobCategoryRepository.UpdateAsync(jc);
            await jobCategoryRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateJobCategoryDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
