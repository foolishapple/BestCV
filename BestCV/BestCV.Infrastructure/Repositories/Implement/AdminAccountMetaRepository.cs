using BestCV.Core.Repositories;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class AdminAccountMetaRepository : RepositoryBaseAsync<AdminAccountMeta, int, JobiContext>, IAdminAccountMetaRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public AdminAccountMetaRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 11/8/2023
        /// Description: kiểm tra trong bảng AdminAccount meta có cặp key, value này không
        /// <param name="code"></param>
        /// <param name="hash"></param>
        /// <returns>kiểm tra trong bảng AdminAccount meta có cặp key, value này không</returns>
        public async Task<AdminAccountMeta?> CheckVerifyCode(string code, string hash)
        {
            var data = (await FindByCondition(x => x.Value == code && x.Description == hash && x.Active).FirstOrDefaultAsync());
            return data;
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 11/8/2023
        /// Description: đếm số lần email đã gửi trong 1 h
        /// </summary>
        /// <param name="AdminAccountId"></param>
        /// <returns>Số lần email đã gửi</returns>
        public async Task<int> CountResetPassword(long AdminAccountId)
        {
            var count = await db.AdminAccountMetas.CountAsync(
            e => e.Active
            && e.AdminAccountId == AdminAccountId
            && e.Key == AdminAccountMetaConstants.FORGOT_PASSWORD_EMAIL_KEY
            && e.CreatedTime <= DateTime.Now
            && e.CreatedTime >= DateTime.Now.AddHours(-(AdminAccountMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT))
            );
            return count;
        }

    }
}
