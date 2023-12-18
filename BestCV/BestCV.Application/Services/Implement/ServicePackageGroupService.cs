using AutoMapper;
using BestCV.Application.Models.EmployerBenefit;
using BestCV.Application.Models.ServicePackageGroup;
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
    public class ServicePackageGroupService : IServicePackageGroupService
    {
        private readonly IServicePackageGroupRepository servicePackageGroupRepository;
        private readonly ILogger<IServicePackageGroupService> logger;
        private readonly IMapper mapper;
        public ServicePackageGroupService(IServicePackageGroupRepository _servicePackageGroupRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            servicePackageGroupRepository = _servicePackageGroupRepository;
            logger = loggerFactory.CreateLogger<IServicePackageGroupService>();
            mapper = _mapper;
        }
        public async Task<BestCVResponse> CreateAsync(InsertServicePackageGroupDTO obj)
        {
            var data = mapper.Map<ServicePackageGroup>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await servicePackageGroupRepository.IsExistedAsync(0, data.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await servicePackageGroupRepository.CreateAsync(data);
            await servicePackageGroupRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertServicePackageGroupDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await servicePackageGroupRepository.FindByConditionAsync(x=>x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<ServicePackageGroupDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await servicePackageGroupRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<ServicePackageGroupDTO>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await servicePackageGroupRepository.SoftDeleteAsync(id);
            if (data)
            {
                await servicePackageGroupRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);
            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateServicePackageGroupDTO obj)
        {
            var data = await servicePackageGroupRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var update = mapper.Map(obj, data);
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await servicePackageGroupRepository.IsExistedAsync(update.Id, update.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await servicePackageGroupRepository.UpdateAsync(update);

            await servicePackageGroupRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateServicePackageGroupDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
