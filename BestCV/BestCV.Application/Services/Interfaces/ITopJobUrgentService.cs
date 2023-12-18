using BestCV.Application.Models.TopJobExtra;
using BestCV.Application.Models.TopJobUrgent;
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
    public interface ITopJobUrgentService : IServiceQueryBase<long, InsertTopJobUrgentDTO, UpdateTopJobUrgentDTO, TopJobUrgentDTO>
    {
        Task<BestCVResponse> ListTopJobUrgent();
        Task<BestCVResponse> ChangeOrderSort(ChangeOrderSortTopJobUrgentDTO model);
        Task<BestCVResponse> ListTopJobUrgentShowOnHomePageAsync();
        Task<BestCVResponse> SearchingUrgentJob(SearchJobWithServiceParameters parameter);
    }
}
