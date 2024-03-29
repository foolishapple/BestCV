using AutoMapper;
using BestCV.Application.Models.JobReference;
using BestCV.Application.Models.JobSuitable;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
    public class JobReferenceService : IJobReferenceService
    {
        private readonly IJobReferenceRepository repository;
        private readonly ILogger<IJobReferenceService> logger;
        private readonly IMapper mapper;
        public JobReferenceService(IJobReferenceRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<IJobReferenceService>();
            mapper = _mapper;
        }

        public async Task<BestCVResponse> CreateAsync(InsertJobReferenceDTO obj)
        {
            // Kiểm tra xem JobId đã tồn tại trong JobSuitable hay chưa
            var isJobIdExist = await repository.IsJobIdExistAsync(obj.JobId);
            if (isJobIdExist)
            {
                var errorList = new List<string>
            {
                "Tên công việc đã tồn tại."
            };
                return BestCVResponse.BadRequest(errorList);
            }

            var jobReference = mapper.Map<JobReference>(obj);
            jobReference.Active = true;
            jobReference.CreatedTime = DateTime.Now;

            var listErrors = new List<string>();

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            await repository.CreateAsync(jobReference);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertJobReferenceDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await repository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<JobReferenceDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<JobReferenceDTO>(data);

            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> ListAggregatesAsync()
        {
            var result = await repository.ListAggregatesAsync();
            if (result == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", result);
            }
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> ListJobReferenceOnDetailJob(long jobId)
        {
            var data = await repository.ListJobReferenceOnDetailJob(jobId);
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<List<SelectListItem>> ListJobSelected()
        {
            return await repository.ListJobSelected();
        }

        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var data = await repository.SoftDeleteAsync(id);
            if (data)
            {
                await repository.SaveChangesAsync();
                return BestCVResponse.Success();

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateJobReferenceDTO obj)
        {
            var jobReference = await repository.GetByIdAsync(obj.Id);
            if (jobReference == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }

            // Kiểm tra chỉ khi JobId thay đổi
            if (obj.JobId != jobReference.JobId)
            {
                var isJobIdExist = await repository.IsJobIdExistAsync(obj.JobId);
                if (isJobIdExist)
                {
                    var errorList = new List<string>
                    {
                        "Tên công việc đã tồn tại."
                    };
                    return BestCVResponse.BadRequest(errorList);
                }
            }

            var updateJobReference = mapper.Map(obj, jobReference);

            var listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await repository.UpdateAsync(updateJobReference);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateJobReferenceDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
