using AutoMapper;
using BestCV.Application.Models.MultimediaType;
using BestCV.Application.Models.NotificationType;
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
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly INotificationTypeRepository notificationTypeRepository;
        private readonly ILogger<NotificationTypeService> logger;
        private readonly IMapper mapper;
        public NotificationTypeService(INotificationTypeRepository notificationTypeRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = loggerFactory.CreateLogger<NotificationTypeService>();
            this.notificationTypeRepository = notificationTypeRepository;
        }


        public async Task<BestCVResponse> CreateAsync(InsertNotificationTypeDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await notificationTypeRepository.IsNameExistAsync(0, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");

            }

            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var newObj = mapper.Map<NotificationType>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = !string.IsNullOrEmpty(newObj.Description) ? newObj.Description.ToEscape() : null;

            await notificationTypeRepository.CreateAsync(newObj);
            await notificationTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(newObj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertNotificationTypeDTO> objs)
        {
            throw new NotImplementedException();
        }

  
        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await notificationTypeRepository.FindByConditionAsync(c => c.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", data);
            }
            var temp = mapper.Map<List<NotificationTypeDTO>>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await notificationTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu.", id);
            }
            var temp = mapper.Map<NotificationTypeDTO>(data);
            return BestCVResponse.Success(temp);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await notificationTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                await notificationTypeRepository.SaveChangesAsync();
                return BestCVResponse.Success(id);
            }
            return BestCVResponse.NotFound("Không có dữ liệu.", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateNotificationTypeDTO obj)
        {
            var listErrors = new List<string>();
            var isNameExist = await notificationTypeRepository.IsNameExistAsync(obj.Id, obj.Name.Trim());
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count>0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            var data = await notificationTypeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không tìm thấy dữ liệu.", obj);
            }
            var updateObj = mapper.Map(obj, data);
            updateObj.Description = !string.IsNullOrEmpty(updateObj.Description) ? updateObj.Description.ToEscape() : null;

            await notificationTypeRepository.UpdateAsync(updateObj);
            await notificationTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);

        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateNotificationTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
