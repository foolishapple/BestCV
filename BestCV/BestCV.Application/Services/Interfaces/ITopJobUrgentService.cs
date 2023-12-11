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
        Task<DionResponse> ListTopJobUrgent();
        Task<DionResponse> ChangeOrderSort(ChangeOrderSortTopJobUrgentDTO model);
        Task<DionResponse> ListTopJobUrgentShowOnHomePageAsync();
        Task<DionResponse> SearchingUrgentJob(SearchJobWithServiceParameters parameter);
    }
}
