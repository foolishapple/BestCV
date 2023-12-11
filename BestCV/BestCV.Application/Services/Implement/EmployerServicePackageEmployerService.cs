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

        public Task<DionResponse> CreateAsync(InsertEmployerServicePackageEmployerDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertEmployerServicePackageEmployerDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateAsync(UpdateEmployerServicePackageEmployerDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateEmployerServicePackageEmployerDTO> obj)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 17/09/2023
        /// Description: Get list employer service package
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GroupEmployerService(DTEmployerServicePackageEmployerParameters parameters)
        {
            var res = await _employerServicePackageEmployerRepository.GroupByParameters(parameters);
            return DionResponse.Success(res);
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 17/09/2023
        /// Description: Get list employer service package add on
        /// </summary>
        /// <returns></returns>
        public async Task<DionResponse> GroupEmployerServiceAddOn(DTEmployerServicePackageEmployerParameters parameters)
        {
            parameters.EmployerServicePackageTypeId = new int[] { ServicePackageTypeId.ADD_ON, ServicePackageTypeId.PREMIUM };
            var res = await _employerServicePackageEmployerRepository.GroupByParameters(parameters);
            return DionResponse.Success(res);
        }
    }
}
