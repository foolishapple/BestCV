using BestCV.Application.Models.TopJobManagement;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.TopJobManagement;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ITopJobManagementService : IServiceQueryBase<long, InsertTopJobManagementDTO, UpdateTopJobManagementDTO, TopJobManagementDTO>
    {
        Task<BestCVResponse> ListAggregatesAsync();
        Task<List<SelectListItem>> ListJobSelected();
        Task<BestCVResponse> SearchingManagementJob(SearchJobWithServiceParameters parameter);
    }
}
