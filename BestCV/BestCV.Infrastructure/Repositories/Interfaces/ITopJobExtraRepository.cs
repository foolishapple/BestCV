using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.TopJobExtra;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ITopJobExtraRepository : IRepositoryBaseAsync<TopJobExtra, long, JobiContext>
    {
        Task<List<TopJobExtraAggregates>> ListTopJobExtra();
        Task<bool> IsJobIdExist(long id, long jobId);
        Task<bool> CheckOrderSort(long Id, int orderSort);
        Task<int> MaxOrderSort(int orderSort);
        Task ChangeOrderSort(List<TopJobExtra> objs);
        /// <summary>
        /// Description: Get mex order sort
        /// </summary>
        /// <returns></returns>
        Task<int> MaxOrderSort();
        /// <summary>
        /// Description: Check job is existed
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<bool> IsExisted(long id);
        Task<List<TopJobExtraAggregates>> ListTopJobExtraShowOnHomePageAsync();
        Task<PagingData<List<TopJobExtraAggregates>>> SearchingFeatureJob(SearchJobWithServiceParameters parameter);
    }
}
