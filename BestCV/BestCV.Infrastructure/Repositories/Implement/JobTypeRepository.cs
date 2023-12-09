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
    public class JobTypeRepository : RepositoryBaseAsync<JobType, int, JobiContext>, IJobTypeRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public JobTypeRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author: DucNN
        /// CreatedTime : 27/07/2023
        /// Description : check name exist 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsNameExistAsync(string name, int id)
        {
            return await db.JobTypes.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);
        }
    }
}
