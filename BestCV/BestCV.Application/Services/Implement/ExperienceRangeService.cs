using AutoMapper;
using BestCV.Application.Models.ExperienceRange;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
	public class ExperienceRangeService : IExperienceRangeService
	{
		private readonly IExperienceRangeRepository experienceRangeRepository;
		private readonly ILogger<IExperienceRangeService> logger;
		private readonly IMapper mapper;
		public ExperienceRangeService(IExperienceRangeRepository _experienceRangeRepository, ILoggerFactory loggerFactory, IMapper _mapper)
		{
			experienceRangeRepository = _experienceRangeRepository;
			logger = loggerFactory.CreateLogger<IExperienceRangeService>();
			mapper = _mapper;
		}

		public async Task<BestCVResponse> CreateAsync(InsertExperienceRangeDTO obj)
		{
			var experienceRange = mapper.Map<ExperienceRange>(obj);
			experienceRange.Active = true;
			experienceRange.CreatedTime = DateTime.Now;
            experienceRange.Description = !string.IsNullOrEmpty(experienceRange.Description) ? experienceRange.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await experienceRangeRepository.IsExperienceRangeExistAsync(experienceRange.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await experienceRangeRepository.CreateAsync(experienceRange);
			var result = await experienceRangeRepository.SaveChangesAsync();
			if (result > 0)
			{
				return BestCVResponse.Success(obj);
			}
			return BestCVResponse.Error("Thêm mới khoảng kinh nghiệm không thành công");
		}

		public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertExperienceRangeDTO> objs)
		{
			throw new NotImplementedException();
		}


		public async Task<BestCVResponse> GetAllAsync()
		{
			var data = await experienceRangeRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<ExperienceRangeDTO>>(data);
			return BestCVResponse.Success(result);

		}


		public async Task<BestCVResponse> GetByIdAsync(int id)
		{
			var data = await experienceRangeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<ExperienceRangeDTO>(data);

            return BestCVResponse.Success(result);
		}

		public async Task<BestCVResponse> SoftDeleteAsync(int id)
		{
			var data = await experienceRangeRepository.SoftDeleteAsync(id);
			if (data)
			{
				//return BestCVResponse.Success();
				await experienceRangeRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);

        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
		{
			throw new NotImplementedException();
		}

		public async Task<BestCVResponse> UpdateAsync(UpdateExperienceRangeDTO obj)
		{
			var experienceRange = await experienceRangeRepository.GetByIdAsync(obj.Id);
			if (experienceRange == null)
			{
				return BestCVResponse.NotFound("Không có dữ liệu", obj);
			}
			var updateExperienceRange = mapper.Map(obj, experienceRange);
            updateExperienceRange.Description = !string.IsNullOrEmpty(updateExperienceRange.Description) ? updateExperienceRange.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await experienceRangeRepository.IsExperienceRangeExistAsync(updateExperienceRange.Name, updateExperienceRange.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await experienceRangeRepository.UpdateAsync(updateExperienceRange);
			await experienceRangeRepository.SaveChangesAsync();
			return BestCVResponse.Success(obj);
		}

		public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateExperienceRangeDTO> obj)
		{
			throw new NotImplementedException();
		}
	}
}
