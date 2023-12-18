using AutoMapper;
using BestCV.Application.Models.CandidateCVPDF;
using BestCV.Application.Models.CandidateCVPDFTypes;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
    public class CandidateCVPDFService : ICandidateCVPDFService
    {
        private readonly ICandidateCVPDFRepository _candidateCVPDFRepository;
        private readonly ILogger<ICandidateCVPDFService> _logger;
        private readonly IMapper _mapper;

        public CandidateCVPDFService(ICandidateCVPDFRepository candidateCVPDFRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _candidateCVPDFRepository = candidateCVPDFRepository;
            _logger = loggerFactory.CreateLogger<ICandidateCVPDFService>();
            _mapper = mapper;

        }
        public async Task<BestCVResponse> CreateAsync(InsertCandidateCVPDFDTO obj)
        {
            var data = _mapper.Map<CandidateCVPDF>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            await _candidateCVPDFRepository.CreateAsync(data);
            await _candidateCVPDFRepository.SaveChangesAsync();
            return BestCVResponse.Success(data);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateCVPDFDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _candidateCVPDFRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = _mapper.Map<List<CandidateCVPDFDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByCandidateId(long candidateId)
        {
            var data = await _candidateCVPDFRepository.FindByConditionAsync(x=>x.CandidateId == candidateId && x.Active);
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = _mapper.Map<List<CandidateCVPDFDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await _candidateCVPDFRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = _mapper.Map<CandidateCVPDFDTO>(data);

            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var data = await _candidateCVPDFRepository.SoftDeleteAsync(id);
            if (data)
            {
                await _candidateCVPDFRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateCandidateCVPDFDTO obj)
        {
            var data = await _candidateCVPDFRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var update = _mapper.Map(obj, data);
            

            await _candidateCVPDFRepository.UpdateAsync(update);

            await _candidateCVPDFRepository.SaveChangesAsync();

            return BestCVResponse.Success(update);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateCVPDFDTO> obj)
        {
            throw new NotImplementedException();
        }
        public async Task<BestCVResponse> UploadCV(UploadCandidateCVPDFDTO model)
        {
            var data = _mapper.Map<CandidateCVPDF>(model);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            await _candidateCVPDFRepository.CreateAsync(data);
            await _candidateCVPDFRepository.SaveChangesAsync();
            return BestCVResponse.Success(data);
        }
    }
}
