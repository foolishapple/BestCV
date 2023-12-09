using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.JobCategory;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IJobCategoryRepository : IRepositoryBaseAsync<JobCategory, int, JobiContext>
    {
        /// <summary>
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">JobCategoryId</param>
        /// <param name="name">JobCategoryName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);

        Task<List<JobCategoryOnHomePageAggregates>> ListCategoryOnHomepage();
    }
}
