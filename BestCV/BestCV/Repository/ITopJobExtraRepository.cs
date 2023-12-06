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
    public interface ITopJobExtraRepository : IRepositoryBaseAsync<TopJobExtra, long, JobiContext>
    {
        Task<List<TopJobExtraAggregates>> ListTopJobExtra();
        Task<bool> IsJobIdExist(long id, long jobId);
        Task<bool> CheckOrderSort(long Id, int orderSort);
        Task<int> MaxOrderSort(int orderSort);
        Task ChangeOrderSort(List<TopJobExtra> objs);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get mex order sort
        /// </summary>
        /// <returns></returns>
        Task<int> MaxOrderSort();
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Check job is existed
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<bool> IsExisted(long id);
        Task<List<TopJobExtraAggregates>> ListTopJobExtraShowOnHomePageAsync();
        Task<PagingData<List<TopJobExtraAggregates>>> SearchingFeatureJob(SearchJobWithServiceParameters parameter);
    }
}
