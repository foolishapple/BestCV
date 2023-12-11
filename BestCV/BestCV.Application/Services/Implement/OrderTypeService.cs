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
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Create new order type
        /// </summary>
        /// <param name="obj">order type DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertOrderTypeDTO obj)
        {
            var item = _mapper.Map<OrderType>(obj);
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
        /// Created: 08/08/2023
        /// Description: Create new list order type
        /// </summary>
        /// <param name="objs">list order type DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertOrderTypeDTO> objs)
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
                return DionResponse.BadRequest(errors);
            }
            await _repository.CreateListAsync(items);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(objs);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Get list all order type
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
        /// Description: Get order type by id
        /// </summary>
        /// <param name="id">order type id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found order type by id: {id}");
            }
            return DionResponse.Success(item);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete order type by id
        /// </summary>
        /// <param name="id">order type by id</param>
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
            throw new Exception($"Not found order type by id: {id}");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete list order type by list order type id
        /// </summary>
        /// <param name="objs">list order type id</param>
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
            throw new Exception($"Failed to soft delete list order type by list order type id");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Update order type
        /// </summary>
        /// <param name="obj">update order type DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateAsync(UpdateOrderTypeDTO obj)
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
                return DionResponse.BadRequest(errors);
            }
            await _repository.UpdateAsync(item);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: update list order type
        /// </summary>
        /// <param name="obj">list order type DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateOrderTypeDTO> obj)
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
                return DionResponse.BadRequest(errors);
            }
            await _repository.UpdateListAsync(updateItems);
            await _repository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Validate to order type
        /// </summary>
        /// <param name="obj">order type object</param>
        /// <returns></returns>
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
