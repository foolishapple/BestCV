using AutoMapper;
using BestCV.Application.Models.SystemConfigs;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class SystemConfigService : ISystemConfigService
    {
        private readonly ISystemConfigRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public SystemConfigService(ISystemConfigRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<SystemConfigService>();
            _mapper = mapper;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Create new system config
        /// </summary>
        /// <param name="obj">system config DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertSystemConfigDTO obj)
        {
            var item = _mapper.Map<SystemConfig>(obj);
            item.Active = true;
            item.CreatedTime = DateTime.Now;
            item.Key = GenerateCode(obj.Key);
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
        /// Created: 08/08/2023
        /// Description: Create new list system config
        /// </summary>
        /// <param name="objs">list system config DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertSystemConfigDTO> objs)
        {
            List<string> errors = new();
            var items = objs.Select(c => _mapper.Map<SystemConfig>(c));
            foreach(var item in items)
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
        /// Created: 08/08/2023
        /// Description: Get list all system config
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await _repository.FindByConditionAsync(c => c.Active);
            return DionResponse.Success(data);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Get system config by id
        /// </summary>
        /// <param name="id">system config id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found system config by id: {id}");
            }
            return DionResponse.Success(item);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete system config by id
        /// </summary>
        /// <param name="id">system config by id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var result =  await _repository.SoftDeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return DionResponse.Success(id);
            }
            throw new Exception($"Not found system config by id: {id}");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete list system config by list system config id
        /// </summary>
        /// <param name="objs">list system config id</param>
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
            throw new Exception($"Failed to soft delete list system config by list system config id");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Update system config
        /// </summary>
        /// <param name="obj">update system config DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateAsync(UpdateSystemConfigDTO obj)
        {
            var item = await _repository.GetByIdAsync(obj.Id);
            if (item == null)
            {
                throw new Exception($"Not found system config by id: {obj.Id}");
            }
            item = _mapper.Map(obj, item);
            item.Key = GenerateCode(obj.Key);
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
        /// Created: 08/08/2023
        /// Description: update list system config
        /// </summary>
        /// <param name="obj">list system config DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateSystemConfigDTO> obj)
        {
            List<SystemConfig> updateItems = new();
            List<string> errors = new();
            foreach(var item in obj)
            {
                var model = await _repository.GetByIdAsync(item.Id);
                if (model == null)
                {
                    throw new Exception($"Not found system config by id: {item.Id}");
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
        /// Created: 08/08/2023
        /// Description: Validate to system config
        /// </summary>
        /// <param name="obj">system config object</param>
        /// <returns></returns>
        private async Task<List<string>> Validate(SystemConfig obj)
        {
            List<string> errors = new();
            if(await _repository.KeyIsExisted(obj.Key, obj.Id))
            {
                errors.Add($"Từ khóa {obj.Key} đã tồn tại.");
            }
            return errors;
        }

        public string GenerateCode(string name)
        {
            name = name.Trim().RemoveVietnamese();
            var nameRegex = Regex.Replace(name, @"\s+", "_");

            return nameRegex.ToUpper();
        }
    }
}
