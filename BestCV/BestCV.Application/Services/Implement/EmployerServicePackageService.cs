using AutoMapper;
using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Models.EmployerServicePackage;
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
    public class EmployerServicePackageService : IEmployerServicePackageService
    {
        private readonly IEmployerServicePackageRepository employerServicePackageRepository;
        private readonly ILogger<IEmployerServicePackageRepository> logger;
        private readonly IMapper mapper;
        public EmployerServicePackageService(IEmployerServicePackageRepository _employerServicePackageRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            employerServicePackageRepository = _employerServicePackageRepository;
            logger = loggerFactory.CreateLogger<IEmployerServicePackageRepository>();
            mapper = _mapper;
        }
        public async Task<BestCVResponse> CreateAsync(InsertEmployerServicePackageDTO obj)
        {
            var employerServicePackage = mapper.Map<EmployerServicePackage>(obj);
            employerServicePackage.Active = true;
            employerServicePackage.CreatedTime = DateTime.Now;
            employerServicePackage.Description = !string.IsNullOrEmpty(employerServicePackage.Description) ? employerServicePackage.Description.ToEscape() : null;

            var listErrors = new List<string>();
            var isNameExist = await employerServicePackageRepository.IsEmployerServicePackageRepositoryExist(0 , employerServicePackage.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên gói đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            await employerServicePackageRepository.CreateAsync(employerServicePackage);
            await employerServicePackageRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertEmployerServicePackageDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await employerServicePackageRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<EmployerServicePackageDTO>>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await employerServicePackageRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<EmployerServicePackageDTO>(data);
            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetListAggregate()
        {
            var data = await employerServicePackageRepository.GetListAggregate();
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            var data = await employerServicePackageRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return BestCVResponse.Success();
                await employerServicePackageRepository.SaveChangesAsync();
                return BestCVResponse.Success(data);

            }
            return BestCVResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> UpdateAsync(UpdateEmployerServicePackageDTO obj)
        {
            var employerServicePackage = await employerServicePackageRepository.GetByIdAsync(obj.Id);
            if (employerServicePackage == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", obj);
            }
            var newEmployerServicePackage = mapper.Map(obj, employerServicePackage);
            employerServicePackage.Description = !string.IsNullOrEmpty(employerServicePackage.Description) ? employerServicePackage.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await employerServicePackageRepository.IsEmployerServicePackageRepositoryExist(newEmployerServicePackage.Id, newEmployerServicePackage.Name);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }

            await employerServicePackageRepository.UpdateAsync(newEmployerServicePackage);

            await employerServicePackageRepository.SaveChangesAsync();

            return BestCVResponse.Success(obj);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateEmployerServicePackageDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
