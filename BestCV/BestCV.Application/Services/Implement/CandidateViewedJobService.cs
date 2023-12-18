using AutoMapper;
using BestCV.Application.Models.CandidateViewedJob;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using BestCV.Domain.Aggregates.CandidateViewedJob;
using BestCV.Domain.Aggregates.CandidateViewJobs;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class CandidateViewedJobService : ICandidateViewedJobService
    {
        private readonly ICandidateViewedJobRepository repository;
        private readonly ILogger<ICandidateViewedJobService> logger;
        private readonly IMapper mapper;

        public CandidateViewedJobService(ICandidateViewedJobRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<ICandidateViewedJobService>();
            mapper = _mapper;
        }

        public Task<BestCVResponse> CreateAsync(InsertCandidateViewedJobDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateViewedJobDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetListViewedJobByCanddiateId(long candidateId)
        {
            var dataCandidate = await repository.ListCandidateViewedJobByCandidateId(candidateId);

            return BestCVResponse.Success(dataCandidate);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var res = await repository.SoftDeleteAsync(id);
            await repository.SaveChangesAsync();
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

        public Task<BestCVResponse> UpdateAsync(UpdateCandidateViewedJobDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateViewedJobDTO> obj)
        {
            throw new NotImplementedException();
        }

        public Task<DTResult<CandidateViewedJobAggregates>> PagingByCandidateId(DTPagingCandidateViewedJobParameters parameters)
        {
            return repository.PagingByCandidateId(parameters);
        }
        
        public async Task<DTResult<CandidateViewedJobAggreagate>> DTPaging(DTCandidateViewedJobParameters parameters)
        {
            return await repository.DTPaging(parameters);
        }
    }
}
