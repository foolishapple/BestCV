using AutoMapper;
using BestCV.Application.Models.EmployerBenefit;
using BestCV.Application.Models.ServicePackageType;
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
    public class ServicePackageTypeService : IServicePackageTypeService
    {
        private readonly IServicePackageTypeRepository servicePackageTypeRepository;
        private readonly ILogger<IServicePackageTypeService> logger;
        private readonly IMapper mapper;
        public ServicePackageTypeService(IServicePackageTypeRepository _servicePackageTypeRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            servicePackageTypeRepository = _servicePackageTypeRepository;
            logger = loggerFactory.CreateLogger<IServicePackageTypeService>();
            mapper = _mapper;
        }
        public async Task<BestCVResponse> CreateAsync(InsertServicePackageTypeDTO obj)
        {
            var data = mapper.Map<ServicePackageType>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await servicePackageTypeRepository.IsExistedAsync(0, data.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await servicePackageTypeRepository.CreateAsync(data);
            await servicePackageTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertServicePackageTypeDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await servicePackageTypeRepository.FindByConditionAsync(x=>x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<ServicePackageTypeDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await servicePackageTypeRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<ServicePackageTypeDTO>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await servicePackageTypeRepository.SoftDeleteAsync(id);
            if (data)
            {
                await servicePackageTypeRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);
            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateServicePackageTypeDTO obj)
        {
            var data = await servicePackageTypeRepository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var update = mapper.Map(obj, data);
            data.Description = !string.IsNullOrEmpty(data.Description) ? data.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await servicePackageTypeRepository.IsExistedAsync(update.Id, update.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await servicePackageTypeRepository.UpdateAsync(update);

            await servicePackageTypeRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateServicePackageTypeDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
