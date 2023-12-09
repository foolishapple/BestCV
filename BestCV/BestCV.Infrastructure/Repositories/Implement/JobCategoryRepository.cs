using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.JobCategory;
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
    public class JobCategoryRepository : RepositoryBaseAsync<JobCategory, int, JobiContext>, IJobCategoryRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public JobCategoryRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            this.db = db;
            this.unitOfWork = unitOfWork;

        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">JobCategoryId</param>
        /// <param name="name">JobCategoryName</param>
        /// <returns>bool</returns>
        public async Task<bool> IsNameExistAsync(int id, string name)
        {
            return await db.JobCategories.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);
        }

        public async Task<List<JobCategoryOnHomePageAggregates>> ListCategoryOnHomepage()
        {
            //return await db.JobCategories.Where(x=>x.Active).OrderBy(x=>x.CreatedTime).Take(6).ToListAsync();
            return await (from row in db.JobCategories
                          where row.Active
                          orderby row.CreatedTime
                          select new JobCategoryOnHomePageAggregates
                          {
                              Id = row.Id,
                              Name = row.Name,
                              CreatedTime = row.CreatedTime,
                              Active = row.Active,
                              Description = row.Description,
                              CountJob = db.Jobs.Where(x=>x.Active && x.IsApproved && x.PrimaryJobCategoryId == row.Id).Count(),
                              Icon = row.Icon
                          }).Take(6).ToListAsync();
        }
    }
}
