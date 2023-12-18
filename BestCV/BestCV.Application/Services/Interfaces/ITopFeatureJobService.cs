
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
        //Task<BestCVResponse> ListTopFeatureJobShowOnHomePageAsync();
        Task<List<SelectListItem>> ListJobSelected();
        //Task<List<TopFeatureJobAggregates>> searchJobs(Select2Aggregates select2Aggregates);
        Task<BestCVResponse> searchJobs(Select2Aggregates select2Aggregates);
        Task<BestCVResponse> ListTopFeatureJobShowOnHomePageAsync();
        Task<BestCVResponse> SearchingFeatureJob(SearchJobWithServiceParameters parameter);
        Task<BestCVResponse> ListFeatureJob();
        Task<BestCVResponse> ChangeOrderSort(ChangeTopFeatureJobDTO model);
    }
}
