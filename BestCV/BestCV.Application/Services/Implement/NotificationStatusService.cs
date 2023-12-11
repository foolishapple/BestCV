using AutoMapper;
using BestCV.Application.Models.NotificationStatuses;
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
    public class NotificationStatusService : INotificationStatusService
    {
        private readonly INotificationStatusRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public NotificationStatusService(INotificationStatusRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<NotificationStatusService>();
            _mapper = mapper;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Create new notification status
        /// </summary>
        /// <param name="obj">notification status DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertNotificationStatusDTO obj)
        {
            var item = _mapper.Map<NotificationStatus>(obj);
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
        /// Description: Create new list notification status
        /// </summary>
        /// <param name="objs">list notification status DTO</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateListAsync(IEnumerable<InsertNotificationStatusDTO> objs)
        {
            List<string> errors = new();
            var items = objs.Select(c => _mapper.Map<NotificationStatus>(c));
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
        /// Description: Get list all notification status
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
        /// Description: Get notification status by id
        /// </summary>
        /// <param name="id">notification status id</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new Exception($"Not found notification status by id: {id}");
            }
            return DionResponse.Success(item);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete notification status by id
        /// </summary>
        /// <param name="id">notification status by id</param>
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
            throw new Exception($"Not found notification status by id: {id}");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Soft delete list notification status by list notification status id
        /// </summary>
        /// <param name="objs">list notification status id</param>
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
            throw new Exception($"Failed to soft delete list notification status by list notification status id");
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Update notification status
        /// </summary>
        /// <param name="obj">update notification status DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateAsync(UpdateNotificationStatusDTO obj)
        {
            var item = await _repository.GetByIdAsync(obj.Id);
            if (item == null)
            {
                throw new Exception($"Not found notification status by id: {obj.Id}");
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
        /// Description: update list notification status
        /// </summary>
        /// <param name="obj">list notification status DTO</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found</exception>
        public async Task<DionResponse> UpdateListAsync(IEnumerable<UpdateNotificationStatusDTO> obj)
        {
            List<NotificationStatus> updateItems = new();
            List<string> errors = new();
            foreach (var item in obj)
            {
                var model = await _repository.GetByIdAsync(item.Id);
                if (model == null)
                {
                    throw new Exception($"Not found notification status by id: {item.Id}");
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
        /// Description: Validate to notification status
        /// </summary>
        /// <param name="obj">notification status object</param>
        /// <returns></returns>
        private async Task<List<string>> Validate(NotificationStatus obj)
        {
            List<string> errors = new();
            if (await _repository.NameIsExisted(obj.Name, obj.Id))
            {
                errors.Add($"Tên {obj.Name} đã tồn tại.");
            }
            if (await _repository.ColorIsExisted(obj.Color, obj.Id))
            {
                errors.Add($"Màu {obj.Name} đã tồn tại.");
            }
            return errors;
        }
    }
}
