using AutoMapper;
using BestCV.Application.Models.FieldOfActivities;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
	public class FieldOfActivityService : IFieldOfActivityService
	{
		private readonly IFieldOfActivityRepository _repository;
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		public FieldOfActivityService(IFieldOfActivityRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
		{
			_repository = repository;
			_logger = loggerFactory.CreateLogger<FieldOfActivityService>();
			_mapper = mapper;
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Create new Field Of Activity
		/// </summary>
		/// <param name="obj">Field Of Activity DTO</param>
		/// <returns></returns>
		public async Task<DionResponse> CreateAsync(InsertFieldOfActivityDTO obj)
		{
			var item = _mapper.Map<FieldOfActivity>(obj);
			item.Active = true;
			item.CreatedTime = DateTime.Now;
			var errors = await Validate(item);
			if (errors.Count > 0)
			{
				return DionResponse.BadRequest(errors);
			}
			await _repository.CreateAsync(item);
			await _repository.SaveChangesAsync();
			return DionResponse.Success(obj);
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Create new list Field Of Activity
		/// </summary>
		/// <param name="objs">list Field Of Activity DTO</param>
		/// <returns></returns>
		public async Task<DionResponse> CreateListAsync(IEnumerable<InsertFieldOfActivityDTO> objs)
		{
			List<string> errors = new();
			var items = objs.Select(c => _mapper.Map<FieldOfActivity>(c));
			foreach (var item in items)
			{
				item.Active = true;
				item.CreatedTime = DateTime.Now;
				errors.AddRange(await Validate(item));
			}
			if (errors.Count > 0)
			{
				return DionResponse.BadRequest(errors);
			}
			await _repository.CreateListAsync(items);
			await _repository.SaveChangesAsync();
			return DionResponse.Success(objs);
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Get list all Field Of Activity
		/// </summary>
		/// <returns></returns>
		public async Task<DionResponse> GetAllAsync()
		{
			var data = await _repository.FindByConditionAsync(c => c.Active);
			return DionResponse.Success(data);
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Get Field Of Activity by id
		/// </summary>
		/// <param name="id">Field Of Activity id</param>
		/// <returns></returns>
		/// <exception cref="Exception">Not found</exception>
		public async Task<DionResponse> GetByIdAsync(int id)
		{
			var item = await _repository.GetByIdAsync(id);
			if (item == null)
			{
				throw new Exception($"Not found Field Of Activity by id: {id}");
			}
			return DionResponse.Success(item);
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Soft delete Field Of Activity by id
		/// </summary>
		/// <param name="id">Field Of Activity by id</param>
		/// <returns></returns>
		/// <exception cref="Exception">Not found</exception>
		public async Task<DionResponse> SoftDeleteAsync(int id)
		{
			var result = await _repository.SoftDeleteAsync(id);
			if (result)
			{
				await _repository.SaveChangesAsync();
				return DionResponse.Success(id);
			}
			throw new Exception($"Not found Field Of Activity by id: {id}");
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Soft delete list Field Of Activity by list Field Of Activity id
		/// </summary>
		/// <param name="objs">list Field Of Activity id</param>
		/// <returns></returns>
		/// <exception cref="Exception">Failed delete</exception>
		public async Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
		{
			var result = await _repository.SoftDeleteListAsync(objs);
			if (result)
			{
				await _repository.SaveChangesAsync();
				return DionResponse.Success(objs);
			}
			throw new Exception($"Failed to soft delete list Field Of Activity by list Field Of Activity id");
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Update Field Of Activity
		/// </summary>
		/// <param name="obj">update Field Of Activity DTO</param>
		/// <returns></returns>
		/// <exception cref="Exception">Not found</exception>
		public async Task<DionResponse> UpdateAsync(UpdateFieldOfActivityDTO obj)
		{
			var item = await _repository.GetByIdAsync(obj.Id);
			if (item == null)
			{
				throw new Exception($"Not found Field Of Activity by id: {obj.Id}");
			}
			item = _mapper.Map(obj, item);
			var errors = await Validate(item);
			if (errors.Count > 0)
			{
				return DionResponse.BadRequest(errors);
			}
			await _repository.UpdateAsync(item);
			await _repository.SaveChangesAsync();
			return DionResponse.Success(obj);
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: update list Field Of Activity
		/// </summary>
		/// <param name="obj">list Field Of Activity DTO</param>
		/// <returns></returns>
		/// <exception cref="Exception">Not found</exception>
		public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateFieldOfActivityDTO> obj)
		{
			List<FieldOfActivity> updateItems = new();
			List<string> errors = new();
			foreach (var item in obj)
			{
				var model = await _repository.GetByIdAsync(item.Id);
				if (model == null)
				{
					throw new Exception($"Not found Field Of Activity by id: {item.Id}");
				}
				model = _mapper.Map(item, model);
				updateItems.Add(model);
				errors.AddRange(await Validate(model));
			}
			if (errors.Count > 0)
			{
				return DionResponse.BadRequest(errors);
			}
			await _repository.UpdateListAsync(updateItems);
			await _repository.SaveChangesAsync();
			return DionResponse.Success(obj);
		}
		/// <summary>
		/// Author: TUNGTD
		/// Created: 11/08/2023
		/// Description: Validate to Field Of Activity
		/// </summary>
		/// <param name="obj">Field Of Activity object</param>
		/// <returns></returns>
		private async Task<List<string>> Validate(FieldOfActivity obj)
		{
			List<string> errors = new();
			if (await _repository.NameIsExisted(obj.Name, obj.Id))
			{
				errors.Add($"Tên {obj.Name} đã tồn tại.");
			}
			return errors;
		}

		/// <summary>
		/// Author: HuyDQ
		/// Created: 16/08/2023
		/// Description: lấy dữ liệu và đếm xem có bảo nhiêu nhà tuyển dụng có lĩnh vực này
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public async Task<DionResponse> LoadDataFilterFielOfActivityHomePageAsync()
        {
			var data = await _repository.LoadDataFilterFielOfActivityHomePageAsync();
			if (data == null)
			{
				return DionResponse.NotFound("Không có dữ liệu", data);
			}
			return DionResponse.Success(data);
		}
	}
}
