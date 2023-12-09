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
    public class CandidateHonorAwardRepository : RepositoryBaseAsync<CandidateHonorAndAward, long, JobiContext>, ICandidateHonorAwardRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;
        public CandidateHonorAwardRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            this.dbContext = _dbContext;
            this.unitOfWord = _unitOfWork;
        }
        /// <summary>
        /// Author : Chung 
        /// Created : 02/08/2023
        /// Description : Lấy ra thông tin giải thưởng của ứng viên 
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<List<CandidateHonorAndAward>> ListCandidateHonorAwardByCandidateId(long candidateId)
        {
            var result = await (
                from ha in dbContext.CandidateHonorAndAwards
                join c in dbContext.Candidates on ha.CandidateId equals c.Id
                where (ha.CandidateId == candidateId && ha.Active && c.Active)
                select new CandidateHonorAndAward
                {

                    Id= ha.Id,
                    Name = ha.Name,
                    TimePeriod = ha.TimePeriod,
                    Description = ha.Description,
                }).ToListAsync();
            return result;
        }
    }
}
