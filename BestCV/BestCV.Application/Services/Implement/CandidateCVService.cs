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

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 11/09/2023
        /// Description: Insert CandidateCV
        /// </summary>
        /// <param name="obj">DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertOrUpdateCandidateCVDTO obj)
        {
            var candidateCV = MappingInsertCandidateCV(obj);
            var candidateCVId = await _candidateCVRepository.CreateAsync(candidateCV);
            var result = await _candidateCVRepository.SaveChangesAsync();
            if (result > 0)
            {
                return DionResponse.Success(candidateCVId);
            }
            else
            {
                return DionResponse.Error("Lưu CV không thành công");
            }
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertOrUpdateCandidateCVDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 24/08/2023
        /// Description: Lấy chi tiết CandidateCV theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> GetByIdAsync(long id)
        {
            var data = await _candidateCVRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }

            return DionResponse.Success(data);
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 20/09/2023
        /// Description: Soft delete CandidateCV
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(long id)
        {
            var data = await _candidateCVRepository.SoftDeleteAsync(id);
            if (data)
            {
                if (await _candidateCVRepository.SaveChangesAsync() > 0)
                {
                    return DionResponse.Success();
                }
            }
            return DionResponse.Error();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 11/09/2023
        /// Description: Update CandidateCV
        /// </summary>
        /// <param name="obj">DTO object</param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(InsertOrUpdateCandidateCVDTO obj)
        {
            var candidateCV = await MappingUpdateCandidateCV(obj);
            await _candidateCVRepository.UpdateAsync(candidateCV);
            var result = await _candidateCVRepository.SaveChangesAsync();
            if (result > 0)
            {
                return DionResponse.Success(obj);
            }
            else
            {
                return DionResponse.Error("Lưu CV không thành công");
            }
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<InsertOrUpdateCandidateCVDTO> obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 20/09/2023
        /// Description: Lấy danh sách theo CandidateId
        /// </summary>
        /// <param name="candidateId">CandidateId</param>
        public async Task<DionResponse> GetListAsyncByCandidateId(long candidateId)
        {
            var listCandidateCV = await _candidateCVRepository.GetListAsyncByCandidateId(candidateId);
            return DionResponse.Success(listCandidateCV);
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 20/09/2023
        /// Description: Lấy danh sách Aggregate theo candidateId
        /// </summary>
        /// <param name="candidateId">candidateId</param>
        /// <returns>Danh sách Aggregate theo candidateId</returns>
        public async Task<DionResponse> GetListAggregateAsyncByCandidateId(long candidateId)
        {
            var listCandidateCVAggregate = await _candidateCVRepository.GetListAggregateAsyncByCandidateId(candidateId);
            return DionResponse.Success(listCandidateCVAggregate);
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 11/09/2023
        /// Description: Mapping DTO to insert CandidateCV
        /// </summary>
        /// <param name="obj">DTO object</param>
        public CandidateCV MappingInsertCandidateCV(InsertOrUpdateCandidateCVDTO obj)
        {
            var candidateCV = _mapper.Map<CandidateCV>(obj);
            candidateCV.Id = 0;
            candidateCV.Active = true;
            candidateCV.CreatedTime = DateTime.Now;
            candidateCV.ModifiedTime = DateTime.Now;

            return candidateCV;
        }

        /// <summary>
        /// Author: Daniel
        /// CreatedDate: 20/08/2023
        /// Description: Mapping DTO to update CandidateCV
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
