
using BestCV.Application.Models.TopFeatureJob;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.TopFeatureJob;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ITopFeatureJobService : IServiceQueryBase<int, InsertTopFeatureJobDTO, UpdateTopFeatureJobDTO, TopFeatureJobDTO>
    {
        //Task<DionResponse> ListTopFeatureJobShowOnHomePageAsync();
        Task<List<SelectListItem>> ListJobSelected();
        //Task<List<TopFeatureJobAggregates>> searchJobs(Select2Aggregates select2Aggregates);
        Task<DionResponse> searchJobs(Select2Aggregates select2Aggregates);
        Task<DionResponse> ListTopFeatureJobShowOnHomePageAsync();
        Task<DionResponse> SearchingFeatureJob(SearchJobWithServiceParameters parameter);
        Task<DionResponse> ListFeatureJob();
        Task<DionResponse> ChangeOrderSort(ChangeTopFeatureJobDTO model);
    }
}
