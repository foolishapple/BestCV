using AutoMapper;
using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateNotifications;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateNotification;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class CandidateNotificationService : ICandidateNotificationService
    {
        private readonly ICandidateNotificationRepository _candidateNotificationRepository;
        private readonly ILogger<CandidateNotificationService> _logger;
        private readonly IMapper _mapper;

        public CandidateNotificationService(ICandidateNotificationRepository candidateNotificationRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _candidateNotificationRepository = candidateNotificationRepository;
            _logger = loggerFactory.CreateLogger<CandidateNotificationService>();
            _mapper = mapper;
        }

        public Task<BestCVResponse> CreateAsync(InsertCandidateNotificationDTO obj)
        {
            throw new NotImplementedException();
        }

      
        public Task<DTResult<CandidateNotification>> DTPaging(CandidateNotificationParameter parameters, long candidateId)
        {
            return _candidateNotificationRepository.ListCandidateNotificationByIdAsync(parameters, candidateId);
        }
       
        public async Task<BestCVResponse> MakeAsRead(long id)
        {
            var obj = new CandidateNotification()
            {
                Id = id,
                NotificationStatusId = NotificationConfig.Status.Read
            };
            await _candidateNotificationRepository.MakeAsRead(obj);
            return BestCVResponse.Success();
        }
        public async Task<BestCVResponse> CountUnreadByCandidateId(long id)
        {
            var data = await _candidateNotificationRepository.CountByCandidateId(id , NotificationConfig.Status.Unread);
            return BestCVResponse.Success(data);
        }
        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateNotificationDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<BestCVResponse> ListRecented(long id)
        {
            var data = await _candidateNotificationRepository.ListRecented(id);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await _candidateNotificationRepository.GetByIdAsync(id);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var res = await _candidateNotificationRepository.SoftDeleteAsync(id);
            await _candidateNotificationRepository.SaveChangesAsync();
            if (res)
            {
                return BestCVResponse.Success();
            }
            else
            {
                return BestCVResponse.Error();
            }
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateCandidateNotificationDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateNotificationDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
