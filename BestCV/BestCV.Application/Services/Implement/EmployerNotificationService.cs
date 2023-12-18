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

        public Task<BestCVResponse> CreateAsync(EmployerNotification obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<EmployerNotification> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EmployerNotification>> GetAllByEmployerId(long employerId)
        {
            var list = await notificationRepository.FindByConditionAsync(x => x.Active && x.EmployerId.Equals(employerId));
            return list;
        }

      
        public Task<DTResult<EmployerNotification>> DTPaging(EmployerNotificationParameter parameters, long employerId)
        {
            return notificationRepository.ListEmployerNotificationByEmployerIdAsync(parameters , employerId);
        }

        public async Task<BestCVResponse> MakeAsRead(long id)
        {
            var obj = new EmployerNotification()
            {
                Id = id,
                NotificationStatusId = NotificationConfig.Status.Read
            };
            await notificationRepository.MakeAsRead(obj);
            return BestCVResponse.Success();
        }
        public async Task<BestCVResponse> CountUnreadByEmployerId(long id)
        {
            var data = await notificationRepository.CountByEmployerId(id, NotificationConfig.Status.Unread);
            return BestCVResponse.Success(data);
        }
        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertEmployerNotificationDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await notificationRepository.GetByIdAsync(id);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var res = await notificationRepository.SoftDeleteAsync(id);
            await notificationRepository.SaveChangesAsync();
            if (res)
            {
                return BestCVResponse.Success();
            }
            else
            {
                return BestCVResponse.Error();
            }
        }
        public async Task<BestCVResponse> ListRecented(long id)
        {
            var data = await notificationRepository.ListRecented(id);
            return BestCVResponse.Success(data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(EmployerNotification obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<EmployerNotification> obj)
        {
            throw new NotImplementedException();
        }
    }
}
