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
        public async Task<DionResponse> CreateAsync(InsertEmployerBenefitDTO obj)
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
                return DionResponse.BadRequest(listErrors);
            }
            await employerBenefitRepository.CreateAsync(data);
            var result = await employerBenefitRepository.SaveChangesAsync();
            if (result > 0)
            {
                return DionResponse.Success(obj);
            }
            return DionResponse.Error("Thêm mới quyền lợi không thành công");
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertEmployerBenefitDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await employerBenefitRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<EmployerBenefitDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await employerBenefitRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<EmployerBenefitDTO>(data);

            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetListBenefitExept(List<int> listData)
        {
            var data = await employerBenefitRepository.FindByConditionAsync(x => x.Active && !listData.Contains(x.Id));
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<EmployerBenefitDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await employerBenefitRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return DionResponse.Success();
                await employerBenefitRepository.SaveChangesAsync();
                return DionResponse.Success(data);

            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateEmployerBenefitDTO obj)
        {
            var data = await employerBenefitRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
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
                return DionResponse.BadRequest(listErrors);
            }
            await employerBenefitRepository.UpdateAsync(result);
            await employerBenefitRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateEmployerBenefitDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
