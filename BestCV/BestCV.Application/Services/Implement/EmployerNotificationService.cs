using AutoMapper;
using BestCV.Application.Models.EmployerActivityLogTypes;
using BestCV.Application.Models.EmployerNotification;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.EmployerNotification;
using BestCV.Domain.Constants;
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
    public class EmployerNotificationService : IEmployerNotificationService
    {
        private readonly IEmployerNotificationRepository notificationRepository;
        private readonly ILogger<EmployerNotificationService> logger;
        private readonly IMapper mapper;
        public EmployerNotificationService(IEmployerNotificationRepository _notificationRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            notificationRepository = _notificationRepository;
            logger = loggerFactory.CreateLogger<EmployerNotificationService>();
            mapper = _mapper;
        }

        public Task<DionResponse> CreateAsync(EmployerNotification obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<EmployerNotification> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EmployerNotification>> GetAllByEmployerId(long employerId)
        {
            var list = await notificationRepository.FindByConditionAsync(x => x.Active && x.EmployerId.Equals(employerId));
            return list;
        }
        /// <summary>
        /// Author : Thoai Anh
        /// Created : 21/08/2023
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
      
        public Task<DTResult<EmployerNotification>> DTPaging(EmployerNotificationParameter parameters, long employerId)
        {
            return notificationRepository.ListEmployerNotificationByEmployerIdAsync(parameters , employerId);
        }
        /// <summary>
        /// Author : Thoai Anh 
        /// CreatedDate : 14/08/2023 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> MakeAsRead(long id)
        {
            var obj = new EmployerNotification()
            {
                Id = id,
                NotificationStatusId = NotificationConfig.Status.Read
            };
            await notificationRepository.MakeAsRead(obj);
            return DionResponse.Success();
        }
        public async Task<DionResponse> CountUnreadByEmployerId(long id)
        {
            var data = await notificationRepository.CountByEmployerId(id, NotificationConfig.Status.Unread);
            return DionResponse.Success(data);
        }
        public Task<DionResponse> CreateListAsync(IEnumerable<InsertEmployerNotificationDTO> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// author: truongthieuhuyen
        /// created: 09.08.2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns>lấy notification theo id</returns>
        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await notificationRepository.GetByIdAsync(id);
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> SoftDeleteAsync(long id)
        {
            var res = await notificationRepository.SoftDeleteAsync(id);
            await notificationRepository.SaveChangesAsync();
            if (res)
            {
                return DionResponse.Success();
            }
            else
            {
                return DionResponse.Error();
            }
        }
        public async Task<DionResponse> ListRecented(long id)
        {
            var data = await notificationRepository.ListRecented(id);
            return DionResponse.Success(data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateAsync(EmployerNotification obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<EmployerNotification> obj)
        {
            throw new NotImplementedException();
        }
    }
}
