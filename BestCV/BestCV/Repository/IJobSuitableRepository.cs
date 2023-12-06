using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Job;
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
    public interface IJobSuitableRepository : IRepositoryBaseAsync<JobSuitable, long, JobiContext>
    {
        Task<List<JobSuitableAggregates>> ListAggregatesAsync();
        Task<List<SelectListItem>> ListJobSelected();
        Task<bool> IsJobIdExistAsync(long jobId);
        Task<List<JobSuitableAggregates>> ListJobSuitableDashboard();
        Task<PagingData<List<JobSuitableAggregates>>> SearchingJobSuitable(SearchJobWithServiceParameters parameter);
    }
}
