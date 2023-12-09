using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Job;
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
    public interface IJobSuitableRepository : IRepositoryBaseAsync<JobSuitable, long, JobiContext>
    {
        Task<List<JobSuitableAggregates>> ListAggregatesAsync();
        Task<List<SelectListItem>> ListJobSelected();
        Task<bool> IsJobIdExistAsync(long jobId);
        Task<List<JobSuitableAggregates>> ListJobSuitableDashboard();
        Task<PagingData<List<JobSuitableAggregates>>> SearchingJobSuitable(SearchJobWithServiceParameters parameter);
    }
}
