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

        public Task<BestCVResponse> CreateAsync(CVTemplate obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<CVTemplate> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await _cvTemplateRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }

            return BestCVResponse.Success(data);
        }

        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(CVTemplate obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<CVTemplate> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllPublishAsync()
        {
            var data = await _cvTemplateRepository.GetAllPublishAsync();
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }
    }
}
