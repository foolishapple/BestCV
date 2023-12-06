using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class CandidateActivitesRepository : RepositoryBaseAsync<CandidateActivities, long, JobiContext>, ICandidateActivitesRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;
        public CandidateActivitesRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            this.dbContext = _dbContext;
            this.unitOfWord = _unitOfWork;
        }
        /// <summary>
        /// Author :Chung 
        /// Created:02/08/2023
        /// Description : Lấy ra thông tin hoạt động của ứng viên
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<List<CandidateActivities>> ListCandidateActivitesByCandidateId(long candidateId)
        {
           var result = await
                (
                from ca in dbContext.CandidateActivities
                join c in dbContext.Candidates on ca.CandidateId equals c.Id
                where (ca.CandidateId == candidateId && ca.Active && c.Active)
                select  new CandidateActivities
                {
                    Id = ca.Id,
                    Name = ca.Name,
                    TimePeriod = ca.TimePeriod,
                    Description = ca.Description,
                }).ToListAsync();
            return result;
        }
    }
    
}
