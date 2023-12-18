using AutoMapper;
using BestCV.Application.Models.SalaryRange;
using BestCV.Application.Models.SalaryType;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
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
    public class SalaryRangeService : ISalaryRangeService
    {
        private readonly ISalaryRangeRepository salaryRepository;
        private readonly ILogger<ISalaryRangeService> logger;
        private readonly IMapper mapper;
        public SalaryRangeService(ISalaryRangeRepository _salaryRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            salaryRepository = _salaryRepository;
            logger = loggerFactory.CreateLogger<ISalaryRangeService>();
            mapper = _mapper;
        }
        public async Task<BestCVResponse> CreateAsync(InsertSalaryRangeDTO obj)
        {
            var salaryRange = mapper.Map<SalaryRange>(obj);
            salaryRange.Active = true;
            salaryRange.CreatedTime = DateTime.Now;
            salaryRange.Description = !string.IsNullOrEmpty(salaryRange.Description) ? salaryRange.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await salaryRepository.IsSalaryRangeExistAsync(salaryRange.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await salaryRepository.CreateAsync(salaryRange);
            await salaryRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertSalaryRangeDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await salaryRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<SalaryRangeDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await salaryRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<SalaryRangeDTO>(data);

            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await salaryRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return BestCVResponse.Success();
                await salaryRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateSalaryRangeDTO obj)
        {
            var salaryRange = await salaryRepository.GetByIdAsync(obj.Id);
            if (salaryRange == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateSalary = mapper.Map(obj, salaryRange);
            salaryRange.Description = !string.IsNullOrEmpty(salaryRange.Description) ? salaryRange.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await salaryRepository.IsSalaryRangeExistAsync(updateSalary.Name, updateSalary.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            await salaryRepository.UpdateAsync(updateSalary);

            await salaryRepository.SaveChangesAsync();

            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateSalaryRangeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
