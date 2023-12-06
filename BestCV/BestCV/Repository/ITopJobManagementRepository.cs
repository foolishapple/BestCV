using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Job;
using Jobi.Domain.Aggregates.JobSuitable;
using Jobi.Domain.Aggregates.TopJobManagement;
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
    public interface ITopJobManagementRepository : IRepositoryBaseAsync<TopJobManagement, long, JobiContext>
    {
        Task<List<TopJobManagementAggregates>> ListAggregatesAsync();
        Task<bool> IsJobIdExistAsync(long jobId);
        Task<List<SelectListItem>> ListJobSelected();
        Task<PagingData<List<TopJobManagementAggregates>>> SearchingManagementJob(SearchJobWithServiceParameters parameter);
    }
}
