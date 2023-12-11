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
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="obj">InsertSalaryTypeDTO</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> CreateAsync(InsertSalaryTypeDTO obj)
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
                return DionResponse.BadRequest(listErrors);
            }
			await salaryRepository.CreateAsync(salaryType);
			await salaryRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
		}
		public Task<DionResponse> CreateListAsync(IEnumerable<InsertSalaryTypeDTO> objs)
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
			var data = await salaryRepository.FindByConditionAsync(x=>x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<SalaryTypeDTO>>(data);
			return DionResponse.Success(result);
		}


		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">salaryTypeId</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> GetByIdAsync(int id)
		{
			var data = await salaryRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<SalaryTypeDTO>(data);

            return DionResponse.Success(result);
		}
		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="name">salaryTypeName</param>
		/// <param name="id">salaryTypeId</param>
		/// <returns>Boolean</returns>
		public async Task<bool> IsSalaryTypeExist(string name, int id)
		{
			return await salaryRepository.IsSalaryTypeExistAsync(name, id);
		}

		/// <summary>
		/// Author : ThanhNd
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">salaryTypeId</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> SoftDeleteAsync(int id)
		{
			var data = await salaryRepository.SoftDeleteAsync(id);
			if (data)
			{
				//return DionResponse.Success();
				await salaryRepository.SaveChangesAsync();
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
		/// <param name="obj">UpdateSalaryTypeDTO</param>
		/// <returns>DionResponse</returns>
		public async Task<DionResponse> UpdateAsync(UpdateSalaryTypeDTO obj)
		{
			var salaryType = await salaryRepository.GetByIdAsync(obj.Id);
			if (salaryType == null)
			{
				return DionResponse.NotFound("Không có dữ liệu", obj);
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
                return DionResponse.BadRequest(listErrors);
            }

			await salaryRepository.UpdateAsync(updateSalary);

			await salaryRepository.SaveChangesAsync();
			
            return DionResponse.Success(obj);

            //return DionResponse.Error("Cập nhật loại lương không thành công");
		}

		public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateSalaryTypeDTO> obj)
		{
			throw new NotImplementedException();
		}
	}
}
