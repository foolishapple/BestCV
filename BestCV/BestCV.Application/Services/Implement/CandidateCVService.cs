using AutoMapper;
using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateCVs;
using BestCV.Application.Models.SalaryType;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateCVs;
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
    public class CandidateCVService : ICandidateCVService
    {
        private readonly ICandidateCVRepository _candidateCVRepository;
        private readonly ILogger<ICandidateCVService> _logger;
        private readonly IMapper _mapper;
        public CandidateCVService(ICandidateCVRepository candidateCVRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _candidateCVRepository = candidateCVRepository;
            _logger = loggerFactory.CreateLogger<ICandidateCVService>();
            _mapper = mapper;
        }


        public async Task<BestCVResponse> CreateAsync(InsertOrUpdateCandidateCVDTO obj)
        {
            var candidateCV = MappingInsertCandidateCV(obj);
            var candidateCVId = await _candidateCVRepository.CreateAsync(candidateCV);
            var result = await _candidateCVRepository.SaveChangesAsync();
            if (result > 0)
            {
                return BestCVResponse.Success(candidateCVId);
            }
            else
            {
                return BestCVResponse.Error("Lưu CV không thành công");
            }
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertOrUpdateCandidateCVDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await _candidateCVRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }

            return BestCVResponse.Success(data);
        }


        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var data = await _candidateCVRepository.SoftDeleteAsync(id);
            if (data)
            {
                if (await _candidateCVRepository.SaveChangesAsync() > 0)
                {
                    return BestCVResponse.Success();
                }
            }
            return BestCVResponse.Error();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(InsertOrUpdateCandidateCVDTO obj)
        {
            var candidateCV = await MappingUpdateCandidateCV(obj);
            await _candidateCVRepository.UpdateAsync(candidateCV);
            var result = await _candidateCVRepository.SaveChangesAsync();
            if (result > 0)
            {
                return BestCVResponse.Success(obj);
            }
            else
            {
                return BestCVResponse.Error("Lưu CV không thành công");
            }
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<InsertOrUpdateCandidateCVDTO> obj)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> GetListAsyncByCandidateId(long candidateId)
        {
            var listCandidateCV = await _candidateCVRepository.GetListAsyncByCandidateId(candidateId);
            return BestCVResponse.Success(listCandidateCV);
        }


        public async Task<BestCVResponse> GetListAggregateAsyncByCandidateId(long candidateId)
        {
            var listCandidateCVAggregate = await _candidateCVRepository.GetListAggregateAsyncByCandidateId(candidateId);
            return BestCVResponse.Success(listCandidateCVAggregate);
        }


        public CandidateCV MappingInsertCandidateCV(InsertOrUpdateCandidateCVDTO obj)
        {
            var candidateCV = _mapper.Map<CandidateCV>(obj);
            candidateCV.Id = 0;
            candidateCV.Active = true;
            candidateCV.CreatedTime = DateTime.Now;
            candidateCV.ModifiedTime = DateTime.Now;

            return candidateCV;
        }


        public async Task<CandidateCV> MappingUpdateCandidateCV(InsertOrUpdateCandidateCVDTO obj)
        {
            var candidateCV = await _candidateCVRepository.GetByIdAsync(obj.Id);
            if (candidateCV == null)
            {
                throw new Exception($"Not found CandidateCV id: {obj.Id}");
            }
            var updateCandidateCV = _mapper.Map(obj, candidateCV);
            updateCandidateCV.ModifiedTime = DateTime.Now;
            return updateCandidateCV;
        }
    }
}
