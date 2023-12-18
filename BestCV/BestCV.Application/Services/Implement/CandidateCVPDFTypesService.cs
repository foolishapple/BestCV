using AutoMapper;
using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateCVPDFTypes;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class CandidateCVPDFTypesService : ICandidateCVPDFTypesService
    {
        private readonly ICandidateCVPDFTypesRepository _candidateCVPDFTypesRepository;
        private readonly ILogger<ICandidateCVPDFTypesService> _logger;
        private readonly IMapper _mapper;

        public CandidateCVPDFTypesService(ICandidateCVPDFTypesRepository candidateCVPDFTypesRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _candidateCVPDFTypesRepository = candidateCVPDFTypesRepository;
            _logger = loggerFactory.CreateLogger<ICandidateCVPDFTypesService>();
            _mapper = mapper;

        }
        public async Task<BestCVResponse> CreateAsync(InsertCandidateCVPDFTypesDTO obj)
        {
            var data = _mapper.Map<CandidateCVPDFType>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await _candidateCVPDFTypesRepository.IsExistAsync(data.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await _candidateCVPDFTypesRepository.CreateAsync(data);
            await _candidateCVPDFTypesRepository.SaveChangesAsync();
            return BestCVResponse.Success(data);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateCVPDFTypesDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _candidateCVPDFTypesRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = _mapper.Map<List<CandidateCVPDFTypesDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await _candidateCVPDFTypesRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = _mapper.Map<CandidateCVPDFTypesDTO>(data);

            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await _candidateCVPDFTypesRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return BestCVResponse.Success();
                await _candidateCVPDFTypesRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateCandidateCVPDFTypesDTO obj)
        {
            var data = await _candidateCVPDFTypesRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var update = _mapper.Map(obj, data);
            update.Description = !string.IsNullOrEmpty(update.Description) ? update.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await _candidateCVPDFTypesRepository.IsExistAsync(update.Name, update.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            await _candidateCVPDFTypesRepository.UpdateAsync(update);

            await _candidateCVPDFTypesRepository.SaveChangesAsync();

            return BestCVResponse.Success(update);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateCVPDFTypesDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
