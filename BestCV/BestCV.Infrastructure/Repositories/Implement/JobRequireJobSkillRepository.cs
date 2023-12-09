using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class JobRequireJobSkillRepository : RepositoryBaseAsync<JobRequireJobSkill, long, JobiContext>, IJobRequireJobSkillRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;

        public JobRequireJobSkillRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            db = dbContext;
            unitOfWork = _unitOfWork;
        }
    }
}
