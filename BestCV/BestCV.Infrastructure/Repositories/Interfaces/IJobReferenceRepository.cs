using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.JobSuitable;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface IJobReferenceRepository : IRepositoryBaseAsync<JobReference,long,JobiContext>
    {
        /// <summary>
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
