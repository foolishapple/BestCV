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

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: add job category
        /// </summary>
        /// <param name="obj">InsertJobCategoryDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> CreateAsync(InsertJobCategoryDTO obj)
        {
            var listErrors = new List<string>();

            var isNameExist = await jobCategoryRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            var jc = mapper.Map<JobCategory>(obj);
            jc.Id = 0;
            jc.Active = true;
            jc.CreatedTime = DateTime.Now;
            jc.Description = !string.IsNullOrEmpty(jc.Description) ? jc.Description.ToEscape() : null;

            await jobCategoryRepository.CreateAsync(jc);
            await jobCategoryRepository.SaveChangesAsync();
            return DionResponse.Success(jc);

        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertJobCategoryDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get list job category 
        /// </summary>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await jobCategoryRepository.FindByConditionAsync(c => c.Active);
            if (data ==null)
            {
                return DionResponse.NotFound("Không tìm thấy dữ liệu", data);
            }
            var temp = mapper.Map<List<JobCategoryDTO>>(data);

            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: get job category by id
        /// </summary>
        /// <param name="id">JobCategoryId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await jobCategoryRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không tìm thấy dữ liệu", data);
            }
            var temp = mapper.Map<JobCategoryDTO>(data);
            return DionResponse.Success(temp);
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: soft delete job category by id
        /// </summary>
        /// <param name="id">JobCategoryId</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await jobCategoryRepository.SoftDeleteAsync(id);
            if (data)
            {
                await jobCategoryRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }
            return DionResponse.NotFound("Không tìm thấy dữ liệu.", id);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: update job category 
        /// </summary>
        /// <param name="obj">UpdateJobCategoryDTO</param>
        /// <returns>DionResponse</returns>
        public async Task<DionResponse> UpdateAsync(UpdateJobCategoryDTO obj)
        {
            var listErrors = new List<string>();

            var isNameExist = await jobCategoryRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count>0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var jc = await jobCategoryRepository.GetByIdAsync(obj.Id);
            if (jc == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateJobCategory = mapper.Map(obj, jc);
            updateJobCategory.Description = !string.IsNullOrEmpty(updateJobCategory.Description) ? updateJobCategory.Description.ToEscape() : null;

            await jobCategoryRepository.UpdateAsync(jc);
            await jobCategoryRepository.SaveChangesAsync();
            return DionResponse.Success(obj);

        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateJobCategoryDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
