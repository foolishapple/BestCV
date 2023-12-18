using AutoMapper;
using BestCV.Application.Models.SalaryType;
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
	public class SalaryTypeService : ISalaryTypeService
	{
		private readonly ISalaryTypeRepository salaryRepository;
		private readonly ILogger<ISalaryTypeService> logger;
		private readonly IMapper mapper;
		public SalaryTypeService(ISalaryTypeRepository _salaryRepository, ILoggerFactory loggerFactory, IMapper _mapper)
		{
			salaryRepository = _salaryRepository;
			logger = loggerFactory.CreateLogger<ISalaryTypeService>();
			mapper = _mapper;
		}

		public async Task<BestCVResponse> CreateAsync(InsertSalaryTypeDTO obj)
		{
			var salaryType = mapper.Map<SalaryType>(obj);
			salaryType.Active = true;
			salaryType.CreatedTime = DateTime.Now;
            salaryType.Description = !string.IsNullOrEmpty(salaryType.Description) ? salaryType.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await salaryRepository.IsSalaryTypeExistAsync(salaryType.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
			await salaryRepository.CreateAsync(salaryType);
			await salaryRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
		}
		public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertSalaryTypeDTO> objs)
		{
			throw new NotImplementedException();
		}

		public async Task<BestCVResponse> GetAllAsync()
		{
			var data = await salaryRepository.FindByConditionAsync(x=>x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<SalaryTypeDTO>>(data);
			return BestCVResponse.Success(result);
		}



		public async Task<BestCVResponse> GetByIdAsync(int id)
		{
			var data = await salaryRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<SalaryTypeDTO>(data);

            return BestCVResponse.Success(result);
		}

		public async Task<bool> IsSalaryTypeExist(string name, int id)
		{
			return await salaryRepository.IsSalaryTypeExistAsync(name, id);
		}


		public async Task<BestCVResponse> SoftDeleteAsync(int id)
		{
			var data = await salaryRepository.SoftDeleteAsync(id);
			if (data)
			{
				//return BestCVResponse.Success();
				await salaryRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);

        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
		{
			throw new NotImplementedException();
		}


		public async Task<BestCVResponse> UpdateAsync(UpdateSalaryTypeDTO obj)
		{
			var salaryType = await salaryRepository.GetByIdAsync(obj.Id);
			if (salaryType == null)
			{
				return BestCVResponse.NotFound("Không có dữ liệu", obj);
			}
			var updateSalary = mapper.Map(obj, salaryType);
            salaryType.Description = !string.IsNullOrEmpty(salaryType.Description) ? salaryType.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await salaryRepository.IsSalaryTypeExistAsync(updateSalary.Name, updateSalary.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

			await salaryRepository.UpdateAsync(updateSalary);

			await salaryRepository.SaveChangesAsync();
			
            return BestCVResponse.Success(obj);

            //return BestCVResponse.Error("Cập nhật loại lương không thành công");
		}

		public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateSalaryTypeDTO> obj)
		{
			throw new NotImplementedException();
		}
	}
}
