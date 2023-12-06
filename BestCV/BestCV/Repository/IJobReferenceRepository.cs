using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.JobSuitable;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IJobReferenceRepository : IRepositoryBaseAsync<JobReference,long,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Check job is existed
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<bool> IsExisted(long id);
        Task<List<JobReferenceAggregates>> ListAggregatesAsync();
        Task<List<SelectListItem>> ListJobSelected();
        Task<bool> IsJobIdExistAsync(long jobId);
        Task<List<JobReferenceAggregates>> ListJobReferenceOnDetailJob(long jobId);
    }
}
