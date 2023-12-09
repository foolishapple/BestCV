using BestCV.Core.Repositories;
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
    public class CandidateCertificateRepository : RepositoryBaseAsync<CandidateCertificate, long, JobiContext>, ICandidateCertificateRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;
        public CandidateCertificateRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            this.dbContext = _dbContext;
            this.unitOfWord = _unitOfWork;
        }
        /// <summary>
        /// Author:Chung 
        /// Created : 02/08/2023 
        /// Description : Lấy ra chứng chỉ của ứng viên
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<List<CandidateCertificate>> ListCandidateCetificateByCandidateId(long candidateId)
        {
            var result = await
                 (
                 from cc in dbContext.CandidateCertificates
                 join c in dbContext.Candidates on cc.CandidateId equals c.Id
                 where (cc.CandidateId == candidateId && cc.Active && c.Active)
                 select new CandidateCertificate
                 {
                     Id = cc.Id,
                     Name = cc.Name,
                     IssueBy = cc.IssueBy,
                     TimePeriod = cc.TimePeriod,
                     Description = cc.Description,
                 }).ToListAsync();
            return result;
        }
    }
}
