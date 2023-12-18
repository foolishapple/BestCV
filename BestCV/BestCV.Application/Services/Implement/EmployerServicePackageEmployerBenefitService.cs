using AutoMapper;
using BestCV.Application.Models.EmployerServicePackageEmployerBenefit;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
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
    public class EmployerServicePackageEmployerBenefitService : IEmployerServicePackageEmployerBenefitService
    {
        private readonly IEmployerServicePackageEmployerBenefitRepository repository;
        private readonly ILogger<IEmployerServicePackageEmployerBenefitService> logger;
        private readonly IMapper mapper;
        public EmployerServicePackageEmployerBenefitService(IEmployerServicePackageEmployerBenefitRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<IEmployerServicePackageEmployerBenefitService>();
            mapper = _mapper;
        }
        public async Task<BestCVResponse> CreateAsync(InsertEmployerServicePackageEmployerBenefitDTO obj)
        {
            var data = mapper.Map<EmployerServicePackageEmployerBenefit>(obj);
            data.Active = true;
            data.CreatedTime = DateTime.Now;

            var listErrors = new List<string>();
            var isNameExist = await repository.IsExistAsync(0,data.EmployerServicePackageId, data.EmployerBenefitId);
            if (isNameExist)
            {
                listErrors.Add("Tên quyền lợi đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await repository.CreateAsync(data);
            await repository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertEmployerServicePackageEmployerBenefitDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetByEmployerServicePackageIdAsync(int id)
        {
            var data = await repository.GetByEmployerServicePackageIdAsync(id);
            if(data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<EmployerServicePackageEmployerBenefitDTO>(data);
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

        public async Task<BestCVResponse> UpdateAsync(UpdateEmployerServicePackageEmployerBenefitDTO obj)
        {
            var data = await repository.GetByIdAsync(obj.Id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateItem = mapper.Map(obj, data);
           
            var listErrors = new List<string>();
            var isNameExist = await repository.IsExistAsync(data.Id, data.EmployerServicePackageId, data.EmployerBenefitId);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            await repository.UpdateAsync(updateItem);

            await repository.SaveChangesAsync();

            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> UpdateHasBenefitAsync(int id)
        {
            var isUpdated = await repository.UpdateHasBenefitAsync(id);
            if (isUpdated)
            {
                await repository.SaveChangesAsync();
                return BestCVResponse.Success(isUpdated);
            }
            return BestCVResponse.BadRequest("Hiển thị quyền lợi không thành công");
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateEmployerServicePackageEmployerBenefitDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
