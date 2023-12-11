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
        /// <summary>
        /// Author : Thoai Anh
        /// CreatedTime : 05/09/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> CreateAsync(InsertCandidateApplyJobSourceDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await _candidateApplyJobSourceRepository.IsNameExistAsync(obj.Name, 0);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var model = mapper.Map<CandidateApplyJobSource>(obj);
            model.Id = 0;
            model.CreatedTime = DateTime.Now;
            model.Active = true;
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await _candidateApplyJobSourceRepository.CreateAsync(model);
            await _candidateApplyJobSourceRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertCandidateApplyJobSourceDTO> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 16/08/2023
        /// Description: Get list all candidate apply job source
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GetAllAsync()
        {
            var data = await _candidateApplyJobSourceRepository.FindByConditionAsync(c => c.Active);
            return DionResponse.Success(data);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await _candidateApplyJobSourceRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<CandidateApplyJobSourceDTO>(data);
            return DionResponse.Success(model);
        }
        /// <summary>
        /// Author : Thoai Anh
        /// CreatedTime : 05/09/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await _candidateApplyJobSourceRepository.SoftDeleteAsync(id);
            if (data)
            {
                await _candidateApplyJobSourceRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }

            return DionResponse.NotFound("Không có dữ liệu. ", id);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author : Thoai Anh
        /// CreatedTime : 05/09/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<DionResponse> UpdateAsync(UpdateCandidateApplyJobSourceDTO obj)
        {
            List<string> listErrors = new List<string>();
            var checkName = await _candidateApplyJobSourceRepository.IsNameExistAsync(obj.Name, obj.Id);
            if (checkName)
            {
                listErrors.Add("Tên đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            var candidateApplyJobSource = await _candidateApplyJobSourceRepository.GetByIdAsync(obj.Id);
            if (candidateApplyJobSource == null)
            {
                return DionResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, candidateApplyJobSource);
            model.Description = !string.IsNullOrEmpty(model.Description) ? model.Description.ToEscape() : null;
            await _candidateApplyJobSourceRepository.UpdateAsync(model);
            await _candidateApplyJobSourceRepository.SaveChangesAsync();
            return DionResponse.Success(model);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateCandidateApplyJobSourceDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
