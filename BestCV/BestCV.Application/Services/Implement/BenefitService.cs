using AutoMapper;
using BestCV.Application.Models.EmployerBenefit;
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
    public class BenefitService : IBenefitService
    {
        private readonly IBenefitRepository benefitRepository;
        private readonly ILogger<IBenefitService> logger;
        private readonly IMapper mapper;
        public BenefitService(IBenefitRepository _benefitRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            benefitRepository = _benefitRepository;
            logger = loggerFactory.CreateLogger<IBenefitService>();
            mapper = _mapper;
        }
        public async Task<DionResponse> CreateAsync(InsertBenefitDTO obj)
        {
            var data = mapper.Map<Benefit>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await benefitRepository.IsExistedAsync(0, data.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await benefitRepository.CreateAsync(data);
            await benefitRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertBenefitDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await benefitRepository.FindByConditionAsync(x=>x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<BenefitDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await benefitRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<BenefitDTO>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await benefitRepository.SoftDeleteAsync(id);
            if (data)
            {
                await benefitRepository.SaveChangesAsync();
                return DionResponse.Success(data);
            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateBenefitDTO obj)
        {
            var data = await benefitRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var update = mapper.Map(obj, data);
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await benefitRepository.IsExistedAsync(update.Id, update.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await benefitRepository.UpdateAsync(update);

            await benefitRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateBenefitDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
