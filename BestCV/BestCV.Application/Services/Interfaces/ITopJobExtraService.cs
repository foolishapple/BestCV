using BestCV.Application.Models.TopJobExtra;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.TopJobExtra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ITopJobExtraService : IServiceQueryBase<long, InsertTopJobExtraDTO, UpdateTopJobExtraDTO, TopJobExtraDTO>
    {
        Task<DionResponse> ListTopJobExtra();
        Task<DionResponse> ChangeOrderSort(ChangeOrderSortDTO model);
        Task<DionResponse> ListTopJobExtraShowOnHomePageAsync();
        Task<DionResponse> SearchingFeatureJob(SearchJobWithServiceParameters parameter);
    }
}
