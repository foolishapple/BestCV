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
        public Task<DionResponse> CreateAsync(InsertEmployerWalletDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertEmployerWalletDTO> objs)
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

        public Task<DionResponse> UpdateAsync(UpdateEmployerWalletDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateEmployerWalletDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetCreditWalletByEmployerId(long employerId)
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
            return DionResponse.Success(result);
        }
    }
}
