using BestCV.Application.Models.EmployerServicePackageEmployers;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.EmployerServicePackageEmployers;
using BestCV.Domain.Constants;
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
    public class EmployerServicePackageEmployerService : IEmployerServicePackageEmployerService
    {
        private readonly IEmployerServicePackageEmployerRepository _employerServicePackageEmployerRepository;
        private readonly ILogger _logger;
        public EmployerServicePackageEmployerService(IEmployerServicePackageEmployerRepository employerServicePackageEmployerRepository, ILoggerFactory loggerFactory)
        {
            _employerServicePackageEmployerRepository = employerServicePackageEmployerRepository;
            _logger = loggerFactory.CreateLogger<EmployerServicePackageEmployer>();
        }

        public Task<BestCVResponse> CreateAsync(InsertEmployerServicePackageEmployerDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertEmployerServicePackageEmployerDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateEmployerServicePackageEmployerDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateEmployerServicePackageEmployerDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GroupEmployerService(DTEmployerServicePackageEmployerParameters parameters)
        {
            var res = await _employerServicePackageEmployerRepository.GroupByParameters(parameters);
            return BestCVResponse.Success(res);
        }

        public async Task<BestCVResponse> GroupEmployerServiceAddOn(DTEmployerServicePackageEmployerParameters parameters)
        {
            parameters.EmployerServicePackageTypeId = new int[] { ServicePackageTypeId.ADD_ON, ServicePackageTypeId.PREMIUM };
            var res = await _employerServicePackageEmployerRepository.GroupByParameters(parameters);
            return BestCVResponse.Success(res);
        }
    }
}
