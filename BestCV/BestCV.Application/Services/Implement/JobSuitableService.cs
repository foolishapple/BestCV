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


        public async Task<BestCVResponse> CreateAsync(InsertJobSuitableDTO obj)
        {
            // Kiểm tra xem JobId đã tồn tại trong JobSuitable hay chưa
            var isJobIdExist = await jobSuitableRepository.IsJobIdExistAsync(obj.JobId);
            if (isJobIdExist)
            {
                var errorList = new List<string>
            {
                "Tên công việc đã tồn tại."
            };
                return BestCVResponse.BadRequest(errorList);
            }

            var jobSuitable = mapper.Map<JobSuitable>(obj);
            jobSuitable.Active = true;
            jobSuitable.CreatedTime = DateTime.Now;

            var listErrors = new List<string>();

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            await jobSuitableRepository.CreateAsync(jobSuitable);
            await jobSuitableRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertJobSuitableDTO> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await jobSuitableRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<JobSuitableDTO>>(data);
            return BestCVResponse.Success(result);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await jobSuitableRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<JobSuitableDTO>(data);

            return BestCVResponse.Success(result);
        }


        public async Task<BestCVResponse> ListAggregatesAsync()
        {
            var result = await jobSuitableRepository.ListAggregatesAsync();
            if (result == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", result);
            }
            return BestCVResponse.Success(result);
        }


        public async Task<List<SelectListItem>> ListJobSelected()
        {
            return await jobSuitableRepository.ListJobSelected();
        }

        public async Task<BestCVResponse> ListJobSuitableDashboard()
        {
            var data = await jobSuitableRepository.ListJobSuitableDashboard();
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SearchingJobSuitable(SearchJobWithServiceParameters parameter)
        {
            var data = await jobSuitableRepository.SearchingJobSuitable(parameter);
            if(data.DataSource == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await jobSuitableRepository.SoftDeleteAsync(id);
            if (data)
            {
                await jobSuitableRepository.SaveChangesAsync();
                return BestCVResponse.Success();

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateJobSuitableDTO obj)
        {
            var jobSuitable = await jobSuitableRepository.GetByIdAsync(obj.Id);
            if (jobSuitable == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
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
                    return BestCVResponse.BadRequest(errorList);
                }
            }
            
            var updateJobSuitable = mapper.Map(obj, jobSuitable);

            var listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await jobSuitableRepository.UpdateAsync(updateJobSuitable);
            await jobSuitableRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateJobSuitableDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> SearchingSuitableJob(SearchJobWithServiceParameters parameter)
        {
            var data = await jobSuitableRepository.SearchingJobSuitable(parameter);
            if (data.DataSource == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }
    }
}
