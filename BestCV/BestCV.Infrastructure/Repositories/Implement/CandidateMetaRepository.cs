using BestCV.Core.Repositories;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CandidateMetaRepository : RepositoryBaseAsync<CandidateMeta, long, JobiContext>, ICandidateMetaRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public CandidateMetaRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// atuhor : HoanNK
        /// CreatedTime : 31/07/2023
        /// Description : Check verify Code 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public async Task<CandidateMeta> CheckVerifyCode(string code, string hash)
        {

            var data = (await FindByCondition(x => x.Value == code && x.Description == hash && x.Active).FirstOrDefaultAsync());
            return data;
        }

        public async Task<int> CountSendingEmail(long id)
        {
            var timeNow = DateTime.Now;
            var limitTimes = CandidateConstants.PASSSWORD_SENT_EMAIL_TIME_LIMIT;
            var fromTime = timeNow.AddDays(-(limitTimes));
            return await db.CandidateMetas.CountAsync(s => s.Active && s.Id == id && s.Key == CandidateConstants.CANDIDATE_META_VERIFY_EMAIL && fromTime <= s.CreatedTime && s.CreatedTime <= timeNow);
        }
        /// <summary>
        ///  Author: DucNN
        /// CreatedDate: 8/08/2023
        /// Description: đếm số lần email đã gửi trong 1 h
        /// </summary>
        /// <param name=""></param>
        /// <returns>Số lần email đã gửi</returns>
        public async Task<int> CountResetPassword(long candidateId)
        {
            return await db.CandidateMetas.CountAsync(
            e => e.Active
            && e.CandidateId == candidateId
            && e.Key == CandidateConstants.FORGOT_PASSWORD_EMAIL_KEY
            && e.CreatedTime <= DateTime.Now
            && e.CreatedTime >= DateTime.Now.AddHours(-(CandidateConstants.FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT))
            );
        }
    }
}
