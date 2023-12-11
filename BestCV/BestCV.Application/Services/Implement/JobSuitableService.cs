using AutoMapper;
using BestCV.Application.Models.Coupon;
using BestCV.Application.Models.JobSuitable;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class JobSuitableService : IJobSuitableService
    {
        private readonly IJobSuitableRepository jobSuitableRepository;
        private readonly ILogger<IJobSuitableService> logger;
        private readonly IMapper mapper;
        public JobSuitableService(IJobSuitableRepository _jobSuitableRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            jobSuitableRepository = _jobSuitableRepository;
            logger = loggerFactory.CreateLogger<IJobSuitableService>();
            mapper = _mapper;
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 8/9/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> CreateAsync(InsertJobSuitableDTO obj)
        {
            // Kiểm tra xem JobId đã tồn tại trong JobSuitable hay chưa
            var isJobIdExist = await jobSuitableRepository.IsJobIdExistAsync(obj.JobId);
            if (isJobIdExist)
            {
                var errorList = new List<string>
            {
                "Tên công việc đã tồn tại."
            };
                return DionResponse.BadRequest(errorList);
            }

            var jobSuitable = mapper.Map<JobSuitable>(obj);
            jobSuitable.Active = true;
            jobSuitable.CreatedTime = DateTime.Now;

            var listErrors = new List<string>();

            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            await jobSuitableRepository.CreateAsync(jobSuitable);
            await jobSuitableRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertJobSuitableDTO> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 8/9/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await jobSuitableRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<JobSuitableDTO>>(data);
            return DionResponse.Success(result);
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 8/9/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await jobSuitableRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<JobSuitableDTO>(data);

            return DionResponse.Success(result);
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created:8/9/2023
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> ListAggregatesAsync()
        {
            var result = await jobSuitableRepository.ListAggregatesAsync();
            if (result == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", result);
            }
            return DionResponse.Success(result);
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created: 8/9/2023
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SelectListItem>> ListJobSelected()
        {
            return await jobSuitableRepository.ListJobSelected();
        }

        public async Task<DionResponse> ListJobSuitableDashboard()
        {
            var data = await jobSuitableRepository.ListJobSuitableDashboard();
            if(data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> SearchingJobSuitable(SearchJobWithServiceParameters parameter)
        {
            var data = await jobSuitableRepository.SearchingJobSuitable(parameter);
            if(data.DataSource == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created:8/9/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await jobSuitableRepository.SoftDeleteAsync(id);
            if (data)
            {
                await jobSuitableRepository.SaveChangesAsync();
                return DionResponse.Success();

            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Nam Anh
        /// Created:8/9/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdateJobSuitableDTO obj)
        {
            var jobSuitable = await jobSuitableRepository.GetByIdAsync(obj.Id);
            if (jobSuitable == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }

            // Kiểm tra chỉ khi JobId thay đổi
            if (obj.JobId != jobSuitable.JobId)
            {
                var isJobIdExist = await jobSuitableRepository.IsJobIdExistAsync(obj.JobId);
                if (isJobIdExist)
                {
                    var errorList = new List<string>
                    {
                        "Tên công việc đã tồn tại."
                    };
                    return DionResponse.BadRequest(errorList);
                }
            }
            
            var updateJobSuitable = mapper.Map(obj, jobSuitable);

            var listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await jobSuitableRepository.UpdateAsync(updateJobSuitable);
            await jobSuitableRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateJobSuitableDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> SearchingSuitableJob(SearchJobWithServiceParameters parameter)
        {
            var data = await jobSuitableRepository.SearchingJobSuitable(parameter);
            if (data.DataSource == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }
    }
}
