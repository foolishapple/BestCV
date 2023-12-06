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
    public class JobStatusRepository : RepositoryBaseAsync<JobStatus, int, JobiContext>, IJobStatusRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public JobStatusRepository(JobiContext dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(dbContext, _unitOfWork)
        {
            _db = dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author: HuyDQ
        /// Created
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsNameExistAsync(string name, int id)
        {
            return await _db.JobStatuses.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);
        }
        public async Task<bool> ColorIsExit(string color ,int id)
        {
            return await _db.JobStatuses.AnyAsync(c => c.Color == color && c.Id != id && c.Active);
        }
    }
}
