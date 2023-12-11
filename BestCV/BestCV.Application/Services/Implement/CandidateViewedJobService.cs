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

        public Task<DionResponse> CreateAsync(InsertCandidateViewedJobDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertCandidateViewedJobDTO> objs)
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

        public async Task<DionResponse> GetListViewedJobByCanddiateId(long candidateId)
        {
            var dataCandidate = await repository.ListCandidateViewedJobByCandidateId(candidateId);

            return DionResponse.Success(dataCandidate);
        }

        public async Task<DionResponse> SoftDeleteAsync(long id)
        {
            var res = await repository.SoftDeleteAsync(id);
            await repository.SaveChangesAsync();
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

        public Task<DionResponse> UpdateAsync(UpdateCandidateViewedJobDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateCandidateViewedJobDTO> obj)
        {
            throw new NotImplementedException();
        }

        public Task<DTResult<CandidateViewedJobAggregates>> PagingByCandidateId(DTPagingCandidateViewedJobParameters parameters)
        {
            return repository.PagingByCandidateId(parameters);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 13/09/2023
        /// Description: datatable paging candidate viewed job parameter
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<DTResult<CandidateViewedJobAggreagate>> DTPaging(DTCandidateViewedJobParameters parameters)
        {
            return await repository.DTPaging(parameters);
        }
    }
}
