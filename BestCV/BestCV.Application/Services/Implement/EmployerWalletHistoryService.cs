using AutoMapper;
using BestCV.Application.Models.EmployerBenefit;
using BestCV.Application.Models.EmployerWalletHistory;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class EmployerWalletHistoryService : IEmployerWalletHistoryService
    {
        private readonly IEmployerWalletHistoriesRepository repository;
        private readonly IEmployerWalletRepository walletRepository;
        private readonly ILogger<IEmployerWalletHistoryService> logger;
        private readonly IMapper mapper;
        public EmployerWalletHistoryService(IEmployerWalletHistoriesRepository _repository, ILoggerFactory loggerFactory, IMapper _mapper, IEmployerWalletRepository walletRepository)
        {
            repository = _repository;
            logger = loggerFactory.CreateLogger<IEmployerWalletHistoryService>();
            mapper = _mapper;
            this.walletRepository = walletRepository;
        }
        public async Task<BestCVResponse> CreateAsync(InsertEmployerWalletHistoryDTO obj)
        {
            using (var trans = await repository.BeginTransactionAsync())
            {
                //thêm vào lịch sử 
                var result = mapper.Map<EmployerWalletHistory>(obj);
                result.IsApproved = false;
                result.UpdatedTime = DateTime.Now;
                await repository.CreateAsync(result);
                if(await repository.SaveChangesAsync() > 0)
                {
                    //trừ tiền trong tài khoản employer wallet tương ứng
                    var wallet = await walletRepository.GetByIdAsync(result.EmployerWalletId);
                    if(wallet != null)
                    {
                        wallet.Value -= result.Amount;
                        await walletRepository.SaveChangesAsync();

                        await trans.CommitAsync();
                        return BestCVResponse.Success();
                    }
                }
                throw new Exception("Failed to use service find cv");
            }
        }
        public async Task<object> ListEmployerWalletHistories(DTParameters parameters)
        {
            return await repository.ListEmployerWalletHistories(parameters);
        }
        public async Task<BestCVResponse> QuickIsApprovedAsync(long id)
        {
            var isUpdated = await repository.QuickIsApprovedAsync(id);
            if (isUpdated)
            {
                await repository.SaveChangesAsync();
                return BestCVResponse.Success(isUpdated);
            }
            return BestCVResponse.BadRequest("Duyệt không thành công");
        }
        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertEmployerWalletHistoryDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await repository.GetByidAggregateAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            

            return BestCVResponse.Success(data);
        }

        public async Task<BestCVResponse> ReportCVCandidate(ReportCandidateDTO model)
        {
            //lấy thông tin ví
            var wallet = await walletRepository.FindByCondition(x=>x.Active && x.EmployerId == model.EmployerId && x.WalletTypeId == EmployerWalletConstants.CREDIT_TYPE).FirstOrDefaultAsync();
            if(wallet == null)
            {
                throw new Exception("Not found wallet by employer id");
            }

            //lấy thông tin lịch sử ví
            var walletHistory = await repository.FindByCondition(x=>x.CandidateId == model.CandidateId && x.Active && x.EmployerWalletId == wallet.Id).FirstOrDefaultAsync();

            if (walletHistory == null)
            {
                throw new Exception("Not found wallet history by employer id");
            }
            //cập nhật 
            walletHistory.WalletHistoryTypeId = WalletHistoryTypeConst.YEU_CAU_HOAN_CREDIT;
            walletHistory.Name = "Yêu cầu hoàn tiền";
            walletHistory.UpdatedTime = DateTime.Now;
            walletHistory.Description = model.Description;
            await repository.UpdateAsync(walletHistory);
            await repository.SaveChangesAsync();

            return BestCVResponse.Success(walletHistory);
        }

        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateAsync(UpdateEmployerWalletHistoryDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateEmployerWalletHistoryDTO> obj)
        {
            throw new NotImplementedException();
        }
    }
}
