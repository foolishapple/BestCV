using AutoMapper;
using BestCV.Application.Models.JobSecondaryJobPositions;
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
    public class JobSecondaryJobCategoryService : IJobSecondaryJobPositionService
    {
        private readonly IJobSecondaryJobCategoryRepository secondaryPositionRepository;
        private readonly ILogger<JobSecondaryJobCategoryService> logger;
        private readonly IMapper mapper;

        public JobSecondaryJobCategoryService(IJobSecondaryJobCategoryRepository _secondaryPositionRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            secondaryPositionRepository = _secondaryPositionRepository;
            logger = loggerFactory.CreateLogger<JobSecondaryJobCategoryService>();
            mapper = _mapper;
        }

        public Task<DionResponse> CreateAsync(InsertJobSecondaryJobCategoryDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertJobSecondaryJobCategoryDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateAsync(UpdateJobSecondaryJobCategoryDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateJobSecondaryJobCategoryDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
