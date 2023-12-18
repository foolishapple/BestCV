using AutoMapper;
using BestCV.Application.Models.JobPosition;
using BestCV.Application.Models.Menu;
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
	public class JobPositionService : IJobPositionService
	{
		private readonly IJobPositionRepository jobPositionRepository;
		private readonly ILogger<IJobPositionService> logger;
		private readonly IMapper mapper;
		public JobPositionService(IJobPositionRepository _jobPositionRepository, ILoggerFactory loggerFactory, IMapper _mapper)
		{
			jobPositionRepository = _jobPositionRepository;
			logger = loggerFactory.CreateLogger<IJobPositionService>();
			mapper = _mapper;
		}

		public async Task<BestCVResponse> CreateAsync(InsertJobPositionDTO obj)
		{
			var jobPosition = mapper.Map<JobPosition>(obj);
			jobPosition.Active = true;
			jobPosition.CreatedTime = DateTime.Now;
            jobPosition.Description = !string.IsNullOrEmpty(jobPosition.Description) ? jobPosition.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await jobPositionRepository.IsJobPositionExistAsync(jobPosition.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await jobPositionRepository.CreateAsync(jobPosition);
			await jobPositionRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

		}

		public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertJobPositionDTO> objs)
		{
			throw new NotImplementedException();
		}


		public async Task<BestCVResponse> GetAllAsync()
		{
			var data = await jobPositionRepository.FindByConditionAsync(x => x.Active);
			if(data == null)
			{
				return BestCVResponse.NotFound("Không có dữ liệu", data);
			}
			var result = mapper.Map<List<JobPosition>>(data);
			return BestCVResponse.Success(result);
		}

		public async Task<BestCVResponse> GetByIdAsync(int id)
		{
			var data = await jobPositionRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<JobPosition>(data);
            return BestCVResponse.Success(result);
		}

		public async Task<BestCVResponse> SoftDeleteAsync(int id)
		{
			var data = await jobPositionRepository.SoftDeleteAsync(id);
			if (data)
			{
				await jobPositionRepository.SaveChangesAsync();

				return BestCVResponse.Success(data);
			}
			return BestCVResponse.NotFound("Không có dữ liệu", data);
		}

		public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
		{
			throw new NotImplementedException();
		}


		public async Task<BestCVResponse> UpdateAsync(UpdateJobPositionDTO obj)
		{
			var jobPosition = await jobPositionRepository.GetByIdAsync(obj.Id);
			if (jobPosition == null)
			{
				return BestCVResponse.NotFound("Không có dữ liệu", obj);
			}
			var updateJobPosition = mapper.Map(obj, jobPosition);

            var listErrors = new List<string>();
            var isNameExist = await jobPositionRepository.IsJobPositionExistAsync(updateJobPosition.Name, obj.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

			await jobPositionRepository.UpdateAsync(updateJobPosition);
			await jobPositionRepository.SaveChangesAsync();
			return BestCVResponse.Success(obj);

		}

		public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateJobPositionDTO> obj)
		{
			throw new NotImplementedException();
		}
	}
}
