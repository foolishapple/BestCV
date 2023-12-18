using AutoMapper;
using BestCV.Application.Models.CandidateApplyJobSources;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class CandidateApplyJobSourceService : ICandidateApplyJobSourceService
    {
        private readonly ICandidateApplyJobSourceRepository _candidateApplyJobSourceRepository;
        private readonly ILogger _logger;
        private readonly IMapper mapper;
        public CandidateApplyJobSourceService(ICandidateApplyJobSourceRepository candidateApplyJobSourceRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            _candidateApplyJobSourceRepository = candidateApplyJobSourceRepository;
            _logger = loggerFactory.CreateLogger<CandidateApplyJobSourceService>();
            mapper = _mapper;
        }
        public async Task<BestCVResponse> CreateAsync(InsertCandidateApplyJobSourceDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await _candidateApplyJobSourceRepository.IsNameExistAsync(obj.Name, 0);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var model = mapper.Map<CandidateApplyJobSource>(obj);
            model.Id = 0;
            model.CreatedTime = DateTime.Now;
            model.Active = true;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await _candidateApplyJobSourceRepository.CreateAsync(model);
            await _candidateApplyJobSourceRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCandidateApplyJobSourceDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _candidateApplyJobSourceRepository.FindByConditionAsync(c => c.Active);
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await _candidateApplyJobSourceRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<CandidateApplyJobSourceDTO>(data);
            return BestCVResponse.Success(model);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await _candidateApplyJobSourceRepository.SoftDeleteAsync(id);
            if (data)
            {
                await _candidateApplyJobSourceRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);
            }

            return BestCVResponse.NotFound("Không có dữ liệu. ", id);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateCandidateApplyJobSourceDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await _candidateApplyJobSourceRepository.IsNameExistAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var candidateApplyJobSource = await _candidateApplyJobSourceRepository.GetByIdAsync(obj.Id);
            if (candidateApplyJobSource == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, candidateApplyJobSource);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await _candidateApplyJobSourceRepository.UpdateAsync(model);
            await _candidateApplyJobSourceRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCandidateApplyJobSourceDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
