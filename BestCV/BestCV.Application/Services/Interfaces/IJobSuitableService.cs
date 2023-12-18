using BestCV.Application.Models.JobSuitable;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.JobSuitable;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IJobSuitableService : IServiceQueryBase<int, InsertJobSuitableDTO, UpdateJobSuitableDTO, JobSuitableDTO>
    {
        Task<BestCVResponse> ListAggregatesAsync();
        Task<List<SelectListItem>> ListJobSelected();
        Task<BestCVResponse> ListJobSuitableDashboard();
        Task<BestCVResponse> SearchingJobSuitable(SearchJobWithServiceParameters parameter);
    }
}
