using AutoMapper;
using BestCV.Application.Models.CompanyFieldOfActivities;
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
    public class CompanyFieldOfActivityService : ICompanyFieldOfActivityService
    {
        private readonly ICompanyFieldOfActivityRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public CompanyFieldOfActivityService(ICompanyFieldOfActivityRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<CompanyFieldOfActivityService>();
            _mapper = mapper;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 11/08/2023
        /// Description: Create new Company Field Of Activity
        /// </summary>
        /// <param name="obj">Company Field Of Activity DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertCompanyFieldOfActivityDTO obj)
        {
            var item = _mapper.Map<CompanyFieldOfActivity>(obj);
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
        /// Description: Create new list Company Field Of Activity
        /// </summary>
        /// <param name="objs">list Company Field Of Activity DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertCompanyFieldOfActivityDTO> objs)
        {
            List<string> errors = new();
            var items = objs.Select(c => _mapper.Map<CompanyFieldOfActivity>(c));
            foreach (var item in items)
            {
                if (!await _repository.IsExisted(0, item.CompanyId, item.FieldOfActivityId))
                {
                    item.Active = true;
                    item.CreatedTime = DateTime.Now;
                    errors.AddRange(await Validate(item));
                }
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
        /// Description: Get list all Company Field Of Activity
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
        /// Description: Get Company Field Of Activity by id
        /// </summary>
        /// <param name="id">Company Field Of Activity id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found Company Field Of Activity by id: {id}");
            }
            return DionResponse.Success(item);
        }

        /// <summary>
        /// Author: HuyDQ
        /// Created: 14/08/2023
        /// Description: get list Company Field Of Activity by company id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<DionResponse> GetFieldActivityByCompanyId(int companyId)
        {
            var item = await _repository.GetFieldActivityByCompanyId(companyId);
            if (item == null)
            {
                throw new Exception($"Not found Company Field Of Activity by id: {companyId}");
            }
            return DionResponse.Success(item);
        }

        /// <summary>
        /// Author: HuyDQ
        /// Created: 14/08/2023
        /// Description: delete list Company Field Of Activity by company id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<DionResponse> DeleteFieldActivityByCompanyId(int companyId)
        {
            var item = await _repository.SoftDeleteByCompanyIdAsync(companyId);
            if (item == false)
            {
                throw new Exception($"Not found Company Field Of Activity by id: {companyId}");
            }
            await _repository.SaveChangesAsync();
            return DionResponse.Success(item);
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 11/08/2023
        /// Description: Hard delete Company Field Of Activity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> HardDeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found Company Field Of Activity by id: {id}");
            }
            await _repository.HardDeleteAsync(id);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(id);
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 11/08/2023
        /// Description: Soft delete Company Field Of Activity by id
        /// </summary>
        /// <param name="id">Company Field Of Activity by id</param>
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
            throw new Exception($"Not found Company Field Of Activity by id: {id}");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 11/08/2023
        /// Description: Soft delete list Company Field Of Activity by list Company Field Of Activity id
        /// </summary>
        /// <param name="objs">list Company Field Of Activity id</param>
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
            throw new Exception($"Failed to soft delete list Company Field Of Activity by list Company Field Of Activity id");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 11/08/2023
        /// Description: Update Company Field Of Activity
        /// </summary>
        /// <param name="obj">update Company Field Of Activity DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateAsync(UpdateCompanyFieldOfActivityDTO obj)
        {
            var item = await _repository.GetByIdAsync(obj.Id);
            if (item == null)
            {
                throw new Exception($"Not found Company Field Of Activity by id: {obj.Id}");
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
        /// Description: update list Company Field Of Activity
        /// </summary>
        /// <param name="obj">list Company Field Of Activity DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateCompanyFieldOfActivityDTO> obj)
        {
            List<CompanyFieldOfActivity> updateItems = new();
            List<string> errors = new();
            foreach (var item in obj)
            {
                var model = await _repository.GetByIdAsync(item.Id);
                if (model == null)
                {
                    throw new Exception($"Not found Company Field Of Activity by id: {item.Id}");
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
        /// Description: Validate to Company Field Of Activity
        /// </summary>
        /// <param name="obj">Company Field Of Activity object</param>
        /// <returns></returns>
        private async Task<List<string>> Validate(CompanyFieldOfActivity obj)
        {
            List<string> errors = new();
            return errors;
        }
    }
}
