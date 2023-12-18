using AutoMapper;
using BestCV.Application.Models.JobSkill;
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
	public class JobSkillService : IJobSkillService
	{
		private readonly IJobSkillRepository jobSkillRepository;
		private readonly ILogger<IJobSkillService> logger;
		private readonly IMapper mapper;
		public JobSkillService(IJobSkillRepository _jobSkillRepository, ILoggerFactory loggerFactory, IMapper _mapper)
		{
			jobSkillRepository = _jobSkillRepository;
			logger = loggerFactory.CreateLogger<IJobSkillService>();
			mapper = _mapper;
		}

		public async Task<BestCVResponse> CreateAsync(InsertJobSkillDTO obj)
		{
			var jobSkill = mapper.Map<JobSkill>(obj);
			jobSkill.Active = true;
			jobSkill.CreatedTime = DateTime.Now;
			jobSkill.Description = !string.IsNullOrEmpty(jobSkill.Description) ? jobSkill.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await jobSkillRepository.IsJobSkillExistAsync(jobSkill.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await jobSkillRepository.CreateAsync(jobSkill);
			await jobSkillRepository.SaveChangesAsync();
			return BestCVResponse.Success(obj);
		}

		public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertJobSkillDTO> objs)
		{
			throw new NotImplementedException();
		}


		public async Task<BestCVResponse> GetAllAsync()
		{
			var data = await jobSkillRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<JobSkillDTO>>(data);
			return BestCVResponse.Success(result);
		}

		public async Task<BestCVResponse> GetByIdAsync(int id)
		{
			var data = await jobSkillRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<JobSkillDTO>(data);

            return BestCVResponse.Success(result);
		}


		public async Task<BestCVResponse> SoftDeleteAsync(int id)
		{
			var data = await jobSkillRepository.SoftDeleteAsync(id);
			if (data)
			{
				await jobSkillRepository.SaveChangesAsync();
				return BestCVResponse.Success();

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);

        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
		{
			throw new NotImplementedException();
		}


		public async Task<BestCVResponse> UpdateAsync(UpdateJobSkillDTO obj)
		{
			var jobSkill = await jobSkillRepository.GetByIdAsync(obj.Id);
			if (jobSkill == null)
			{
				return BestCVResponse.NotFound("Không có dữ liệu", obj);
			}
			var updateJobSkill = mapper.Map(obj, jobSkill);
            updateJobSkill.Description = !string.IsNullOrEmpty(updateJobSkill.Description) ? updateJobSkill.Description.ToEscape() : null;


            var listErrors = new List<string>();
            var isNameExist = await jobSkillRepository.IsJobSkillExistAsync(updateJobSkill.Name, updateJobSkill.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            await jobSkillRepository.UpdateAsync(updateJobSkill);

			await jobSkillRepository.SaveChangesAsync();
			
			return BestCVResponse.Success(obj);
		}

		public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateJobSkillDTO> obj)
		{
			throw new NotImplementedException();
		}
	}
}
