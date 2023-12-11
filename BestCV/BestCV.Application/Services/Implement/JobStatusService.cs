using AutoMapper;
using BestCV.Application.Models.JobStatuses;
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
    public class JobStatusService : IJobStatusService
    {
        private readonly IJobStatusRepository _JobStatusRepository;
        private readonly ILogger<JobStatusService> _logger;
        private readonly IMapper _mapper;
        public JobStatusService(IJobStatusRepository JobStatusRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _JobStatusRepository = JobStatusRepository;
            _logger = loggerFactory.CreateLogger<JobStatusService>();
            _mapper = mapper;
        }
        /// <summary>
        /// Author: HuyDQ
        /// Created: 27/07/2023
        /// Description: Insert job status
        /// </summary>
        /// <param name="obj">DTO job status</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertJobStatusDTO obj)
        {
            List<string> listErrors = new List<string>();
            var isNameExist = await _JobStatusRepository.IsNameExistAsync(obj.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            var isColorExist = await _JobStatusRepository.ColorIsExit(obj.Color, 0);
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");

            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var model = _mapper.Map<JobStatus>(obj);
            model.Id = 0;
            model.CreatedTime = DateTime.Now;
            model.Active = true;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await _JobStatusRepository.CreateAsync(model);
            await _JobStatusRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }
        
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertJobStatusDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await _JobStatusRepository.FindByConditionAsync(s => s.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = _mapper.Map<List<JobStatusDTO>>(data);

            return DionResponse.Success(model);
        }

        /// <summary>
        /// Author: HuyDQ
        /// Created: 27/07/2023
        /// Description: Get job status by id
        /// </summary>
        /// <param name="id">job status id</param>
        /// <returns></returns>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await _JobStatusRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = _mapper.Map<JobStatusDTO>(data);
            return DionResponse.Success(model);
        }
        /// <summary>
        /// Author: HuyDQ
        /// Created: 27/07/2023
        /// Description: Soft delete job status by id
        /// </summary>
        /// <param name="id">job status id</param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await _JobStatusRepository.SoftDeleteAsync(id);
            if (data)
            {
                await _JobStatusRepository.SaveChangesAsync();
                return DionResponse.Success();
                
            }
            return DionResponse.NotFound("Không có dữ liệu. ", id);
        }
        
        public async Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: HuyDQ
        /// Create: 27/07/2023
        /// Description: Update job status by update job status DTO
        /// </summary>
        /// <param name="obj">Update account DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdateJobStatusDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await _JobStatusRepository.IsNameExistAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            var isColorExist = await _JobStatusRepository.ColorIsExit(obj.Color, obj.Id);
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");

            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var jobStatus = await _JobStatusRepository.GetByIdAsync(obj.Id);
            if (jobStatus == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = _mapper.Map(obj, jobStatus);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await _JobStatusRepository.UpdateAsync(model);
            await _JobStatusRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }
        
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateJobStatusDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
