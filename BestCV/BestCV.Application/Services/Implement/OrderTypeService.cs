using AutoMapper;
using BestCV.Application.Models.OrderTypes;
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
    public class OrderTypeService : IOrderTypeService
    {
        private readonly IOrderTypeRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public OrderTypeService(IOrderTypeRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<OrderTypeService>();
            _mapper = mapper;
        }

        public async Task<BestCVResponse> CreateAsync(InsertOrderTypeDTO obj)
        {
            var item = _mapper.Map<OrderType>(obj);
            item.Active = true;
            item.CreatedTime = DateTime.Now;
            var errors = await Validate(item);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.CreateAsync(item);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertOrderTypeDTO> objs)
        {
            List<string> errors = new();
            var items = objs.Select(c => _mapper.Map<OrderType>(c));
            foreach (var item in items)
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
                throw new Exception($"Not found order type by id: {id}");
            }
            return BestCVResponse.Success(item);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var result = await _repository.SoftDeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return BestCVResponse.Success(id);
            }
            throw new Exception($"Not found order type by id: {id}");
        }

        public async Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            var result = await _repository.SoftDeleteListAsync(objs);
            if (result)
            {
                await _repository.SaveChangesAsync();
                return BestCVResponse.Success(objs);
            }
            throw new Exception($"Failed to soft delete list order type by list order type id");
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateOrderTypeDTO obj)
        {
            var item = await _repository.GetByIdAsync(obj.Id);
            if (item == null)
            {
                throw new Exception($"Not found order type by id: {obj.Id}");
            }
            item = _mapper.Map(obj, item);
            var errors = await Validate(item);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _repository.UpdateAsync(item);
            await _repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateOrderTypeDTO> obj)
        {
            List<OrderType> updateItems = new();
            List<string> errors = new();
            foreach (var item in obj)
            {
                var model = await _repository.GetByIdAsync(item.Id);
                if (model == null)
                {
                    throw new Exception($"Not found order type by id: {item.Id}");
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

        private async Task<List<string>> Validate(OrderType obj)
        {
            List<string> errors = new();
            if (await _repository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add($"Tên {obj.Name} đã tồn tại.");
            }
            return errors;
        }
    }
}
