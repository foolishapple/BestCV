using AutoMapper;
using BestCV.Application.Models.EmployerBenefit;
using BestCV.Application.Models.ExperienceRange;
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
    public class EmployerBenefitService : IEmployerBenefitService
    {
        private readonly IEmployerBenefitRepository employerBenefitRepository;
        private readonly ILogger<IEmployerBenefitService> logger;
        private readonly IMapper mapper;
        public EmployerBenefitService(IEmployerBenefitRepository _employerBenefitRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            employerBenefitRepository = _employerBenefitRepository;
            logger = loggerFactory.CreateLogger<IEmployerBenefitService>();
            mapper = _mapper;
        }
        public async Task<BestCVResponse> CreateAsync(InsertEmployerBenefitDTO obj)
        {
            var data = mapper.Map<EmployerBenefit>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await employerBenefitRepository.IsExistedAsync(0, data.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await employerBenefitRepository.CreateAsync(data);
            var result = await employerBenefitRepository.SaveChangesAsync();
            if (result > 0)
            {
                return BestCVResponse.Success(obj);
            }
            return BestCVResponse.Error("Thêm mới quyền lợi không thành công");
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertEmployerBenefitDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await employerBenefitRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<EmployerBenefitDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await employerBenefitRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<EmployerBenefitDTO>(data);

            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetListBenefitExept(List<int> listData)
        {
            var data = await employerBenefitRepository.FindByConditionAsync(x => x.Active && !listData.Contains(x.Id));
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<EmployerBenefitDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await employerBenefitRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return BestCVResponse.Success();
                await employerBenefitRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateEmployerBenefitDTO obj)
        {
            var data = await employerBenefitRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var result = mapper.Map(obj, data);
            result.Description = !string.IsNullOrEmpty(result.Description) ? result.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await employerBenefitRepository.IsExistedAsync(result.Id, result.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await employerBenefitRepository.UpdateAsync(result);
            await employerBenefitRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateEmployerBenefitDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
