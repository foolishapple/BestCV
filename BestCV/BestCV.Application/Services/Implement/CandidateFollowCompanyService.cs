using AutoMapper;
using BestCV.Application.Models.CandidateFollowCompany;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateFollowCompany;
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
    public class CandidateFollowCompanyService : ICandidateFollowCompanyService
    {
        private readonly ICandidateFollowCompanyRepository repository;
        private readonly ILogger<ICandidateFollowCompanyService> logger;
        private readonly IMapper mapper;

        public CandidateFollowCompanyService(ICandidateFollowCompanyRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<ICandidateFollowCompanyService>();
            mapper = _mapper;
        }

        public Task<BestCVResponse> CreateAsync(InsertCandidateFollowCompanyDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateFollowCompanyDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CandidateFollowCompany> GetAsyncByCandidateIdAndCompanyI(long candidateId, int companyId)
        {
            return await repository.GetAsyncByCandidateIdAndCompanyI(candidateId, companyId);
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await repository.GetByIdAsync(id);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> GetListCompanyByCandidateId(long candidateId)
        {
            var dataCandidate = await repository.ListCandidateFollowCompanyByCandidateId(candidateId);
            return BestCVResponse.Success(dataCandidate);
        }

        public async Task<BestCVResponse> HardDeleteAsync(long id)
        {
            var item = await repository.GetByIdAsync(id);
            if(item == null)
            {
                throw new Exception($"Not found Company Field Of Activity by id: {id}");
            }

            await repository.HardDeleteAsync(id);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(id);
        }

        public async Task<BestCVResponse> InsertCandidateWithViewModel(InsertCandidateFollowCompanyDTO candidateDTO, long candidateID)
        {
            if(await repository.IsCompanyIdExist(candidateID, candidateDTO.companyId))
            {
                var candidateFollowCompany = await repository.GetAsyncByCandidateIdAndCompanyI(candidateID, candidateDTO.companyId);
                if(candidateFollowCompany != null)
                {
                    await repository.HardDeleteAsync(candidateFollowCompany.Id);
                    await repository.SaveChangesAsync();
                    return BestCVResponse.Success(candidateFollowCompany, "Delete");
                }
                return BestCVResponse.BadRequest("");
            }
            else
            {
                var candidateFollowCompanyMap = mapper.Map<CandidateFollowCompany>(candidateDTO);
                candidateFollowCompanyMap.CandidateId = candidateID;
                candidateFollowCompanyMap.CreatedTime = DateTime.Now;
                candidateFollowCompanyMap.Active = true;

                await repository.CreateAsync(candidateFollowCompanyMap);
                await repository.SaveChangesAsync();
                return BestCVResponse.Success(candidateFollowCompanyMap, "Success");
            }
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 29/08/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<DTResult<CandidateFollowCompanyAggregates>> PagingByCandidateId(DTPagingCandidateFollowCompanyParameters parameters)
        {
            return repository.PagingByCandidateId(parameters);
        }

        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateCandidateFollowCompanyDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateFollowCompanyDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
