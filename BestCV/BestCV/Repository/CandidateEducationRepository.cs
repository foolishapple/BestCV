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
    public class CandidateEducationRepository : RepositoryBaseAsync<CandidateEducation,long,JobiContext>, ICandidateEducationRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public CandidateEducationRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }
        /// <summary>
        /// Author : Thoại Anh
        /// Created : 01/08/2023
        /// Description : Lấy ra thông tin giáo dục và đào tạo của ứng viên
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<List<CandidateEducation>> ListCandidateEducationByCandidateId(long candidateId)
        {
            var result = await (
                from ce in dbContext.CandidateEducations
                join c in dbContext.Candidates on ce.CandidateId equals c.Id
                where (ce.CandidateId == candidateId && ce.Active && c.Active)
                select new CandidateEducation
                {
                    Id = ce.Id,
                    Title = ce.Title,
                    School = ce.School,
                    TimePeriod = ce.TimePeriod,
                    Description = ce.Description
                }).ToListAsync();
            return result; 
        }
    }
}
