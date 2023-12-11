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
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="obj">InsertJobPositionDTO</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> CreateAsync(InsertJobPositionDTO obj)
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
                return DionResponse.BadRequest(listErrors);
            }
            await jobPositionRepository.CreateAsync(jobPosition);
			await jobPositionRepository.SaveChangesAsync();
            return DionResponse.Success(obj);

		}

		public Task<DionResponse> CreateListAsync(IEnumerable<InsertJobPositionDTO> objs)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> GetAllAsync()
		{
			var data = await jobPositionRepository.FindByConditionAsync(x => x.Active);
			if(data == null)
			{
				return DionResponse.NotFound("Không có dữ liệu", data);
			}
			var result = mapper.Map<List<JobPosition>>(data);
			return DionResponse.Success(result);
		}

		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">JobPositionId</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> GetByIdAsync(int id)
		{
			var data = await jobPositionRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<JobPosition>(data);
            return DionResponse.Success(result);
		}

		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">jobPositionId</param>
		/// <returns></returns>
		public async Task<DionResponse> SoftDeleteAsync(int id)
		{
			var data = await jobPositionRepository.SoftDeleteAsync(id);
			if (data)
			{
				await jobPositionRepository.SaveChangesAsync();

				return DionResponse.Success(data);
			}
			return DionResponse.NotFound("Không có dữ liệu", data);
		}

		public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="obj">UpdateJobPositionDTO</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> UpdateAsync(UpdateJobPositionDTO obj)
		{
			var jobPosition = await jobPositionRepository.GetByIdAsync(obj.Id);
			if (jobPosition == null)
			{
				return DionResponse.NotFound("Không có dữ liệu", obj);
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
                return DionResponse.BadRequest(listErrors);
            }

			await jobPositionRepository.UpdateAsync(updateJobPosition);
			await jobPositionRepository.SaveChangesAsync();
			return DionResponse.Success(obj);

		}

		public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateJobPositionDTO> obj)
		{
			throw new NotImplementedException();
		}
	}
}
