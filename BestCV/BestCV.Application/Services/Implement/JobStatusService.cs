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

        public async Task<BestCVResponse> CreateAsync(InsertJobStatusDTO obj)
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
                return BestCVResponse.BadRequest(listErrors);
            }
            var model = _mapper.Map<JobStatus>(obj);
            model.Id = 0;
            model.CreatedTime = DateTime.Now;
            model.Active = true;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await _JobStatusRepository.CreateAsync(model);
            await _JobStatusRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }
        
        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertJobStatusDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _JobStatusRepository.FindByConditionAsync(s => s.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = _mapper.Map<List<JobStatusDTO>>(data);

            return BestCVResponse.Success(model);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await _JobStatusRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = _mapper.Map<JobStatusDTO>(data);
            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await _JobStatusRepository.SoftDeleteAsync(id);
            if (data)
            {
                await _JobStatusRepository.SaveChangesAsync();
                return BestCVResponse.Success();
                
            }
            return BestCVResponse.NotFound("Không có dữ liệu. ", id);
        }
        
        public async Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateJobStatusDTO obj)
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
                return BestCVResponse.BadRequest(listErrors);
            }
            var jobStatus = await _JobStatusRepository.GetByIdAsync(obj.Id);
            if (jobStatus == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = _mapper.Map(obj, jobStatus);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await _JobStatusRepository.UpdateAsync(model);
            await _JobStatusRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }
        
        public async Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateJobStatusDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
