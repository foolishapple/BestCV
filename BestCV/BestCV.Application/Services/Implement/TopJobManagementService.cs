using AutoMapper;
using BestCV.Application.Models.TopJobManagement;
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
    public class TopJobManagementService : ITopJobManagementService
    {
        private readonly ITopJobManagementRepository repository ;
        private readonly ILogger<ITopJobManagementService> logger;
        private readonly IMapper mapper;

        public TopJobManagementService(ITopJobManagementRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<ITopJobManagementService>();
            mapper = _mapper;
        }

        public async Task<DionResponse> CreateAsync(InsertTopJobManagementDTO obj)
        {
            // Kiểm tra xem JobId đã tồn tại trong JobSuitable hay chưa
            var isJobIdExist = await repository.IsJobIdExistAsync(obj.JobId);
            if (isJobIdExist)
            {
                var errorList = new List<string>
            {
                "Tên công việc đã tồn tại."
            };
                return DionResponse.BadRequest(errorList);
            }

            var topJobManagement = mapper.Map<TopJobManagement>(obj);
            topJobManagement.Active = true;
            topJobManagement.CreatedTime = DateTime.Now;

            var listErrors = new List<string>();

            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            await repository.CreateAsync(topJobManagement);
            await repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertTopJobManagementDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await repository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<TopJobManagementDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<TopJobManagementDTO>(data);

            return DionResponse.Success(result);
        }

        public async Task<DionResponse> ListAggregatesAsync()
        {
            var result = await repository.ListAggregatesAsync();
            if (result == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", result);
            }
            return DionResponse.Success(result);
        }

        public async Task<List<SelectListItem>> ListJobSelected()
        {
            return await repository.ListJobSelected();
        }

        public async Task<DionResponse> SearchingManagementJob(SearchJobWithServiceParameters parameter)
        {
            var data = await repository.SearchingManagementJob(parameter);
            if(data.DataSource == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> SoftDeleteAsync(long id)
        {
            var data = await repository.SoftDeleteAsync(id);
            if (data)
            {
                await repository.SaveChangesAsync();
                return DionResponse.Success();

            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateTopJobManagementDTO obj)
        {
            var jobSuitable = await repository.GetByIdAsync(obj.Id);
            if (jobSuitable == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }

            // Kiểm tra chỉ khi JobId thay đổi
            if (obj.JobId != jobSuitable.JobId)
            {
                var isJobIdExist = await repository.IsJobIdExistAsync(obj.JobId);
                if (isJobIdExist)
                {
                    var errorList = new List<string>
                    {
                        "Tên công việc đã tồn tại."
                    };
                    return DionResponse.BadRequest(errorList);
                }
            }

            var updateTopJobManagement = mapper.Map(obj, jobSuitable);

            var listErrors = new List<string>();
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await repository.UpdateAsync(updateTopJobManagement);
            await repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateTopJobManagementDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
