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
    public class CandidateProjectsRepository : RepositoryBaseAsync<CandidateProjects, long, JobiContext>, ICandidateProjectsRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public CandidateProjectsRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<List<CandidateProjects>> ListCandidateProjectsByCandidateId(long candidateId)
        {
            var result = await (
                from cp in db.CandidateProjects
                join c in db.Candidates on cp.CandidateId equals c.Id
                where (cp.CandidateId == candidateId && cp.Active && c.Active)
                select new CandidateProjects
                {
                    Id = cp.Id,
                    ProjectName = cp.ProjectName,
                    Customer = cp.Customer,
                    TeamSize = cp.TeamSize,
                    Position = cp.Position,
                    Responsibilities = cp.Responsibilities,
                    Info = cp.Info,
                    TimePeriod = cp.TimePeriod
                }).ToListAsync();
            return result;
        }
    }
}
