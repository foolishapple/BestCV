using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class RefreshJobRepository : RepositoryBaseAsync<RefreshJob, long, JobiContext>, IRefreshJobRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public RefreshJobRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            _db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 02/10/2023
        /// Description: Refresh job by id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        public async Task<bool> RefreshJob(long id)
        {
            var job = new Job()
            {
                Id = id,
                RefreshDate = DateTime.Now
            };
            _db.Jobs.Attach(job);
            _db.Entry(job).Property(c => c.RefreshDate).IsModified = true;
            return (await _db.SaveChangesAsync()) > 0;
        }
    }
}
