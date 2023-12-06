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
    public class TopAreaJobRepository : RepositoryBaseAsync<TopAreaJob, int, JobiContext>, ITopAreaJobRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public TopAreaJobRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            _db = _dbContext;
            unitOfWork = _unitOfWork;
        }

        public async Task<bool> JobIsExisted(long jobId)
        {
            bool result = await _db.TopAreaJobs.AnyAsync(c => c.Active && c.JobId == jobId);
            return result;
        }

        public async Task<int> LastOrderSort()
        {
            var length = await _db.TopAreaJobs.Where(c => c.Active).CountAsync();
            if (length == 0)
            {
                return 0;
            }
            int lastOrderSort = await _db.TopAreaJobs.Where(c => c.Active).Select(c => c.OrderSort).MaxAsync();
            return lastOrderSort;
        }

        public async Task<int> LastSubOrderSort(int orderSort)
        {
            var length = await _db.TopAreaJobs.Where(c => c.Active).CountAsync();
            if (length == 0)
            {
                return 0;
            }
            int lastSubOrderSort = await _db.TopAreaJobs.Where(c => c.Active && c.OrderSort == orderSort).Select(c => c.SubOrderSort).MaxAsync();
            return lastSubOrderSort;
        }
    }
}
