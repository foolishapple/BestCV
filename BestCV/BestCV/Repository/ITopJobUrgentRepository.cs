using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Domain.Aggregates.Job;
using Jobi.Domain.Aggregates.TopJobExtra;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ITopJobUrgentRepository : IRepositoryBaseAsync<TopJobUrgent, long, JobiContext>
    {
        Task<List<TopJobUrgentAggregates>> ListTopJobUrgent();
        Task<bool> IsJobIdExist(long id, long jobId);
        Task<bool> CheckOrderSort(long Id, int orderSort);
        Task<int> MaxOrderSort(int orderSort);
        Task ChangeOrderSort(List<TopJobUrgent> objs);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Check job is existed 
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<bool> IsExisted(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get max order sort
        /// </summary>
        /// <returns></returns>
        Task<int> MaxOrderSort();
        Task<List<TopJobUrgentAggregates>> ListTopJobUrgentShowOnHomePageAsync();
        Task<PagingData<List<TopJobUrgentAggregates>>> SearchingUrgentJob(SearchJobWithServiceParameters parameter);
    }
}
