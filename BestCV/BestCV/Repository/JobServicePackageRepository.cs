using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.JobServicePackages;
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
    public class JobServicePackageRepository : RepositoryBaseAsync<JobServicePackage,long,JobiContext>, IJobServicePackageRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public JobServicePackageRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork):base(db,unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 18/09/2023
        /// Description: List job service package aggregate
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        public async Task<List<JobServicePackageAggregate>> ListAggregate(long id)
        {
            var query = from jsp in _db.JobServicePackages
                        join sp in _db.EmployerServicePackages on jsp.EmployerServicePackageId equals sp.Id
                        join j in _db.Jobs on jsp.JobId equals j.Id
                        where jsp.Active && sp.Active && j.Active && j.Id == id
                        select new JobServicePackageAggregate()
                        {
                            ServicePackageName = sp.Name,
                            CreatedTime = jsp.CreatedTime,
                            ExpireTime = jsp.ExpireTime,
                            Id = jsp.Id,
                            Quantity = jsp.Quantity
                        };
            var result = await query.ToListAsync();
            return result;
        }
    }
}
