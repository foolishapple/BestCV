using AutoMapper;
using BestCV.Application.Models.EmployerWallet;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Constants;
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
    public class EmployerWalletService : IEmployerWalletService
    {
        private readonly IEmployerWalletRepository repository;
        private readonly ILogger<IEmployerWalletService> logger;
        private readonly IMapper mapper;
        public EmployerWalletService(IEmployerWalletRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<IEmployerWalletService>();
            mapper = _mapper;
        }
        public Task<BestCVResponse> CreateAsync(InsertEmployerWalletDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertEmployerWalletDTO> objs)
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

        public Task<BestCVResponse> UpdateAsync(UpdateEmployerWalletDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateEmployerWalletDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetCreditWalletByEmployerId(long employerId)
        {
            var data = await repository.GetCreditWalletByEmployerId(employerId);
            if(data == null)
            {
                data = new EmployerWallet()
                {
                    Active = true,
                    CreatedTime = DateTime.Now,
                    EmployerId = employerId,
                    Value = 0,
                    WalletTypeId = EmployerWalletConstants.CREDIT_TYPE,
                };
                await repository.CreateAsync(data);
                await repository.SaveChangesAsync();
            }
            var result = mapper.Map<EmployerWalletDTO>(data);
            return BestCVResponse.Success(result);
        }
    }
}
