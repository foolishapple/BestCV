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

        public async Task<BestCVResponse> CreateAsync(InsertSystemConfigDTO obj)
        {
            var item = _mapper.Map<SystemConfig>(obj);
            item.Active = true;
            item.CreatedTime = DateTime.Now;
            item.Key = GenerateCode(obj.Key);
            var errors = await Validate(item);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.CreateAsync(item);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertSystemConfigDTO> objs)
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
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.CreateListAsync(items);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(objs);
        }
 
        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _repository.FindByConditionAsync(c => c.Active);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found system config by id: {id}");
            }
            return BestCVResponse.Success(item);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var result =  await _repository.SoftDeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return BestCVResponse.Success(id);
            }
            throw new Exception($"Not found system config by id: {id}");
        }

        public async Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            var result = await _repository.SoftDeleteListAsync(objs);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return BestCVResponse.Success(objs);
            }
            throw new Exception($"Failed to soft delete list system config by list system config id");
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateSystemConfigDTO obj)
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
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.UpdateAsync(item);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateSystemConfigDTO> obj)
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
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.UpdateListAsync(updateItems);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

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
