using AutoMapper;
using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.SalaryType;
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
    public class CVTemplateService : ICVTemplateService
    {
        private readonly ICVTemplateRepository _cvTemplateRepository;
        private readonly ILogger<ICVTemplateService> _logger;
        private readonly IMapper _mapper;
        public CVTemplateService(ICVTemplateRepository cvTemplateRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _cvTemplateRepository = cvTemplateRepository;
            _logger = loggerFactory.CreateLogger<ICVTemplateService>();
            _mapper = mapper;
        }

        public Task<DionResponse> CreateAsync(CVTemplate obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<CVTemplate> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await _cvTemplateRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }

            return DionResponse.Success(data);
        }

        public Task<DionResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateAsync(CVTemplate obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<CVTemplate> obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 22/08/2023
        /// Description: Lấy tất cả các template CV đã publish
        /// </summary>
        public async Task<DionResponse> GetAllPublishAsync()
        {
            var data = await _cvTemplateRepository.GetAllPublishAsync();
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            return DionResponse.Success(data);
        }
    }
}
