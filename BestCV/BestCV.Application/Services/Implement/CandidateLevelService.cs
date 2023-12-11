using AutoMapper;
using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Models.SalaryType;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
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
    public class CandidateLevelService : ICandidateLevelService
    {
        private readonly ICandidateLevelRepository candidateLevelRepository;
        private readonly ILogger<ICandidateLevelService> logger;
        private readonly IMapper mapper;
        public CandidateLevelService(ICandidateLevelRepository _candidateLevelRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            candidateLevelRepository = _candidateLevelRepository;
            logger = loggerFactory.CreateLogger<ICandidateLevelService>();
            mapper = _mapper;
        }
        public async Task<DionResponse> CreateAsync(InsertCandidateLevelDTO obj)
        {
            var candidateLevel = mapper.Map<CandidateLevel>(obj);
            candidateLevel.Active = true;
            candidateLevel.CreatedTime = DateTime.Now;
            candidateLevel.Description = !string.IsNullOrEmpty(candidateLevel.Description) ? candidateLevel.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await candidateLevelRepository.IsCandidateLevelExistAsync(candidateLevel.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên cấp đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await candidateLevelRepository.CreateAsync(candidateLevel);
            await candidateLevelRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertCandidateLevelDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await candidateLevelRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<CandidateLevelDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await candidateLevelRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<CandidateLevelDTO>(data);

            return DionResponse.Success(result);
        }

        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await candidateLevelRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return DionResponse.Success();
                await candidateLevelRepository.SaveChangesAsync();
                return DionResponse.Success(data);

            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateCandidateLevelDTO obj)
        {
            var candidateLevel = await candidateLevelRepository.GetByIdAsync(obj.Id);
            if (candidateLevel == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateSalary = mapper.Map(obj, candidateLevel);
            candidateLevel.Description = !string.IsNullOrEmpty(candidateLevel.Description) ? candidateLevel.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await candidateLevelRepository.IsCandidateLevelExistAsync(updateSalary.Name, updateSalary.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            await candidateLevelRepository.UpdateAsync(updateSalary);

            await candidateLevelRepository.SaveChangesAsync();

            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateCandidateLevelDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
