using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.JobSuitable;
using BestCV.Domain.Aggregates.TopJobManagement;
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
    public interface ITopJobManagementRepository : IRepositoryBaseAsync<TopJobManagement, long, JobiContext>
    {
        Task<List<TopJobManagementAggregates>> ListAggregatesAsync();
        Task<bool> IsJobIdExistAsync(long jobId);
        Task<List<SelectListItem>> ListJobSelected();
        Task<PagingData<List<TopJobManagementAggregates>>> SearchingManagementJob(SearchJobWithServiceParameters parameter);
    }
}
