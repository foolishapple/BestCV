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
    public interface ITopJobUrgentRepository : IRepositoryBaseAsync<TopJobUrgent, long, JobiContext>
    {
        Task<List<TopJobUrgentAggregates>> ListTopJobUrgent();
        Task<bool> IsJobIdExist(long id, long jobId);
        Task<bool> CheckOrderSort(long Id, int orderSort);
        Task<int> MaxOrderSort(int orderSort);
        Task ChangeOrderSort(List<TopJobUrgent> objs);
        /// <summary>
        /// Description: Check job is existed 
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<bool> IsExisted(long id);
        /// <summary>
        /// Description: Get max order sort
        /// </summary>
        /// <returns></returns>
        Task<int> MaxOrderSort();
        Task<List<TopJobUrgentAggregates>> ListTopJobUrgentShowOnHomePageAsync();
        Task<PagingData<List<TopJobUrgentAggregates>>> SearchingUrgentJob(SearchJobWithServiceParameters parameter);
    }
}
