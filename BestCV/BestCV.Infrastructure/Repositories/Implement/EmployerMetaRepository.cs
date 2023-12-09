using BestCV.Core.Repositories;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class EmployerMetaRepository : RepositoryBaseAsync<EmployerMeta, int, JobiContext>, IEmployerMetaRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public EmployerMetaRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 29.07.2023
        /// </summary>
        /// <param name="code"></param>
        /// <param name="hash"></param>
        /// <returns>kiểm tra trong bảng employer meta có cặp key, value này không</returns>
        public async Task<EmployerMeta> CheckVerifyCode(string code, string hash)
        {
            var data = (await FindByCondition(x => x.Value == code && x.Description == hash && x.Active).FirstOrDefaultAsync());
            return data;
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: đếm số lần email đã gửi trong 1 h
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns>Số lần email đã gửi</returns>
        public async Task<int> CountResetPassword(long employerId)
        {
            return await db.EmployerMetas.CountAsync(
            e => e.Active
            && e.EmployerId == employerId
            && e.Key == EmployerMetaConstants.FORGOT_PASSWORD_EMAIL_KEY
            && e.CreatedTime <= DateTime.Now
            && e.CreatedTime >= DateTime.Now.AddHours(-(EmployerMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT))
            );
        }

    }
}
