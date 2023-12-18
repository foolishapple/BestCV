using AutoMapper;
using BestCV.Application.Models.ServicePackageBenefit;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
    public class ServicePackageBenefitService : IServicePackageBenefitService
    {
        private readonly IServicePackageBenefitRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<IServicePackageBenefitService> logger;
        public ServicePackageBenefitService(IServicePackageBenefitRepository repository, IMapper mapper, ILoggerFactory logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger.CreateLogger<IServicePackageBenefitService>();
        }

        public async Task<BestCVResponse> CreateAsync(InsertServicePackageBenefitDTO obj)
        {
            var data = mapper.Map<ServicePackageBenefit>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;
            await repository.CreateAsync(data);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertServicePackageBenefitDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await repository.FindByConditionAsync(x=>x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<ServicePackageBenefit>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<ServicePackageBenefit>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await repository.SoftDeleteAsync(id);
            if (data)
            {
                //return BestCVResponse.Success();
                await repository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateServicePackageBenefitDTO obj)
        {
            var data = await repository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateItem = mapper.Map(obj, data);

            await repository.UpdateAsync(updateItem);

            await repository.SaveChangesAsync();

            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateServicePackageBenefitDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetListByEmployerServicePackage(int employerServicePackageId)
        {
            var data = await repository.ListAggregateByServicePackageId(employerServicePackageId);
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }
    }
}
