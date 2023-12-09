using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class CandidateSkillRepository : RepositoryBaseAsync<CandidateSkill ,long,JobiContext>, ICandidateSkillRepository
    {
        private readonly JobiContext dbContext;
        private readonly IUnitOfWork<JobiContext> unitOfWord;

        public CandidateSkillRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWord = _unitOfWork;
        }
        /// <summary>
        /// Author : Thoại Anh 
        /// Created : 01/08/2023 
        /// Description : Lấy ra kỹ năng của ứng viên
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public async Task<List<CandidateSkill>> ListCandidateSkillByCandidateId(long candidateId)
        {
            var result = await ( 
                from cs in dbContext.CandidateSkills 
                join c in dbContext.Candidates on cs.CandidateId equals c.Id           
                where ( cs.CandidateId == candidateId && cs.Active && c.Active) 
                select new CandidateSkill
                {

                    Id = cs.Id,
                    Name = cs.Name,
                    Description = cs.Description,

                }).ToListAsync();
            return result; 
        }
    }
}
