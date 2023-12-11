using AutoMapper;
using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.SalaryType;
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
    public class AccountStatusService : IAccountStatusService
    {
        private readonly IAccountStatusRepository accountStatusRepository;
        private readonly ILogger<IAccountStatusService> logger;
        private readonly IMapper mapper;
        public AccountStatusService(IAccountStatusRepository _accountStatusRepository, ILoggerFactory loggerFactory, IMapper _mapper)
        {
            accountStatusRepository = _accountStatusRepository;
            logger = loggerFactory.CreateLogger<IAccountStatusService>();
            mapper = _mapper;
        }
        public async Task<DionResponse> CreateAsync(InsertAccountStatusDTO obj)
        {
            var accountStatus = mapper.Map<AccountStatus>(obj);
            accountStatus.Active = true;
            accountStatus.CreatedTime = DateTime.Now;
            accountStatus.Description = !string.IsNullOrEmpty(accountStatus.Description) ? accountStatus.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await accountStatusRepository.IsAccountStatusExistAsync(accountStatus.Name, 0);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }
            var isColorExist = await accountStatusRepository.IsColorExistAsync(accountStatus.Color, accountStatus.Id);
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");
            }
            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }
            await accountStatusRepository.CreateAsync(accountStatus);
            await accountStatusRepository.SaveChangesAsync();
            return DionResponse.Success(obj);
        }

        public Task<DionResponse> CreateListAsync(IEnumerable<InsertAccountStatusDTO> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> GetAllAsync()
        {
            var data = await accountStatusRepository.FindByConditionAsync(x => x.Active);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<List<AccountStatusDTO>>(data);
            return DionResponse.Success(result);
        }

        public async Task<DionResponse> GetByIdAsync(int id)
        {
            var data = await accountStatusRepository.GetByIdAsync(id);
            if (data == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<AccountStatusDTO>(data);

            return DionResponse.Success(result);
        }

        public async Task<DionResponse> SoftDeleteAsync(int id)
        {
            var data = await accountStatusRepository.SoftDeleteAsync(id);
            if (data)
            {
                //return DionResponse.Success();
                await accountStatusRepository.SaveChangesAsync();
                return DionResponse.Success(data);

            }
            return DionResponse.NotFound("Không có dữ liệu", data);
        }

        public Task<DionResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }

        public async Task<DionResponse> UpdateAsync(UpdateAccountStatusDTO obj)
        {
            var accountStatus = await accountStatusRepository.GetByIdAsync(obj.Id);
            if (accountStatus == null)
            {
                return DionResponse.NotFound("Không có dữ liệu", obj);
            }
            var updateSalary = mapper.Map(obj, accountStatus);
            accountStatus.Description = !string.IsNullOrEmpty(accountStatus.Description) ? accountStatus.Description.ToEscape() : null;
            var listErrors = new List<string>();
            var isNameExist = await accountStatusRepository.IsAccountStatusExistAsync(updateSalary.Name, updateSalary.Id);
            if (isNameExist)
            {
                listErrors.Add("Tên đã tồn tại.");
            }

            var isColorExist = await accountStatusRepository.IsColorExistAsync(updateSalary.Color, updateSalary.Id);
            if (isColorExist)
            {
                listErrors.Add("Màu đã tồn tại.");
            }

            if (listErrors.Count > 0)
            {
                return DionResponse.BadRequest(listErrors);
            }

            await accountStatusRepository.UpdateAsync(updateSalary);

            await accountStatusRepository.SaveChangesAsync();

            return DionResponse.Success(obj);

            //return DionResponse.Error("Cập nhật loại lương không thành công");
        }

        public Task<DionResponse> UpdateListAsync(IEnumerable<UpdateAccountStatusDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
