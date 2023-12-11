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

        public Task<DionResponse> CreateAsync(InsertCandidateNotificationDTO obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author :Thoai Anh
        /// CreatedDate : 27/07/2023
        /// Description : Lấy thông báo theo từng ứng viên
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="candidateId">Mã ứng viên</param>
        /// <returns></returns>
        public Task<DTResult<CandidateNotification>> DTPaging(CandidateNotificationParameter parameters, long candidateId)
        {
            return _candidateNotificationRepository.ListCandidateNotificationByIdAsync(parameters, candidateId);
        }
        /// <summary>
        /// Author : Thoai Anh 
        /// CreatedDate : 14/08/2023 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> MakeAsRead(long id)
        {
            var obj = new CandidateNotification()
            {
                Id = id,
                NotificationStatusId = NotificationConfig.Status.Read
            };
            await _candidateNotificationRepository.MakeAsRead(obj);
            return DionResponse.Success();
        }
        public async Task<DionResponse> CountUnreadByCandidateId(long id)
        {
            var data = await _candidateNotificationRepository.CountByCandidateId(id , NotificationConfig.Status.Unread);
            return DionResponse.Success(data);
        }
        public Task<DionResponse> CreateListAsync(IEnumerable<InsertCandidateNotificationDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<DionResponse> ListRecented(long id)
        {
            var data = await _candidateNotificationRepository.ListRecented(id);
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await _candidateNotificationRepository.GetByIdAsync(id);
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> SoftDeleteAsync(long id)
        {
            var res = await _candidateNotificationRepository.SoftDeleteAsync(id);
            await _candidateNotificationRepository.SaveChangesAsync();
            if (res)
            {
                return DionResponse.Success();
            }
            else
            {
                return DionResponse.Error();
            }
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateAsync(UpdateCandidateNotificationDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateCandidateNotificationDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
