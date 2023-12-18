using AutoMapper;
using BestCV.Application.Models.CandidateSaveJob;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.CandidateSaveJob;
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
    public class CandidateSaveJobService : ICandidateSaveJobService
    {
        private readonly ICandidateSaveJobRepository repository;
        private readonly ICandidateRepository repositoryCandidate;
        private readonly ILogger<ICandidateSaveJobService> logger;
        private readonly IMapper mapper;
        public CandidateSaveJobService(ICandidateSaveJobRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper, ICandidateRepository _repositoryCandidate)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<ICandidateSaveJobService>();
            mapper = _mapper;
            repositoryCandidate = _repositoryCandidate;
        }
        public Task<BestCVResponse> CreateAsync(InsertCandidateSaveJobDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateSaveJobDTO> objs)
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

        public async Task<BestCVResponse> GetListJobByCandidateId(long candidateId)
        {
            var dataCandidat =  await repository.ListCandidateSaveJobByCandidateId(candidateId);

            return BestCVResponse.Success(dataCandidat);
        }

        public Task<DTResult<CandidateSaveJobAggregates>> PagingByCandidateId(DTPagingCandidateSaveJobParameters parameters)
        {
            return repository.PagingByCandidateId(parameters);
        }

        public async Task<BestCVResponse> QuickSaveJob(long id, long accountId)
        {
            var dataAccount = await repositoryCandidate.GetByIdAsync(accountId);
            if(dataAccount == null)
            {
                return BestCVResponse.NotFound("Tài khoản không tồn tại", dataAccount);
            }
            //nếu đã tồn tại thì xóa
            if(await repository.IsJobIdExist(accountId, id))
            {
                var saveJob = await repository.GetByCandidateIdIdAndJobIdAsync(accountId, id);
                if(saveJob != null)
                {
                    await repository.HardDeleteAsync(saveJob.Id);
                    await repository.SaveChangesAsync();
                    return BestCVResponse.Success(saveJob, "Deleted");
                }
            }
            //nếu chưa tồn tại thì thêm mới
            var candidateSaveJob = new InsertCandidateSaveJobDTO
            {
                CandidateId = accountId,
                JobId = id,
            };
            var result = mapper.Map<CandidateSaveJob>(candidateSaveJob);
            result.Active = true;
            result.CreatedTime = DateTime.Now;
            result.Id = 0;
            await repository.CreateAsync(result);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(candidateSaveJob);

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

        public Task<BestCVResponse> UpdateAsync(UpdateCandidateSaveJobDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateSaveJobDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
