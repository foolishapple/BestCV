using BestCV.Application.Models.JobReference;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.JobSuitable;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IJobReferenceService : IServiceQueryBase<long, InsertJobReferenceDTO, UpdateJobReferenceDTO, JobReferenceDTO>
    {
        Task<DionResponse> ListAggregatesAsync();
        Task<List<SelectListItem>> ListJobSelected();
        Task<DionResponse> ListJobReferenceOnDetailJob(long jobId);
    }
}
