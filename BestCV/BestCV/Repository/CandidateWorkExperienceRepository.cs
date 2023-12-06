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
    public class CandidateWorkExperienceRepository : RepositoryBaseAsync<CandidateWorkExperience, long , JobiContext>, ICandidateWorkExperienceRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public CandidateWorkExperienceRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author : Thoại Anh
        /// Created : 01/08/2023
        /// Description : Lấy ra kinh nghiệm làm việc của ứng viên
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<List<CandidateWorkExperience>> ListCandidateWorkExperienceByCandidateId(long candidateId)
        {
            var result = await (
                from cw in db.CandidateWorkExperiences
                join c in db.Candidates on cw.CandidateId equals c.Id
                where (cw.CandidateId == candidateId && cw.Active && c.Active)
                select new CandidateWorkExperience
                {
                    Id = cw.Id,
                    JobTitle = cw.JobTitle,
                    Company = cw.Company,
                    TimePeriod = cw.TimePeriod,
                    Description = cw.Description
                }).ToListAsync();
            return result;
        }
    }
}
