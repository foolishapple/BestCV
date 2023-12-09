using BestCV.Core.Entities;
using BestCV.Core.Repositories;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.TopCompany;
using BestCV.Domain.Aggregates.TopFeatureJob;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ITopFeatureJobRepository : IRepositoryBaseAsync<TopFeatureJob, int, JobiContext>
    {
        Task<List<TopFeatureJobAggregates>> ListTopFeatureJobShowOnHomePageAsync();
        Task<List<SelectListItem>> ListJobSelected();
        Task<TopFeatureJob> GetByJobIdAsync(long jobId);
        Task<List<TopFeatureJob>> FindByConditionAsync(Expression<Func<TopFeatureJob, bool>> expression);
        Task<List<TopFeatureJobAggregates>> searchJobs(Select2Aggregates select2Aggregates);
        Task<TopFeatureJob> GetByIdAsync(int id);
        Task<TopFeatureJob> FindByJobIdAsync(long jobId);
        Task<TopFeatureJob> FindByOrderSortAsync(int orderSort);
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
        /// <summary>
        /// Author : Thoại Anh
        /// </summary>
        /// <param name="orderSort"></param>
        /// <returns></returns>
        Task<int> MaxOrderSort(int orderSort);
        Task<List<TopFeatureJobAggregates>> ListTopFeatureJob();
       
        Task<bool> CheckOrderSort(long Id, int orderSort);
        Task ChangeOrderSort(List<TopFeatureJob> objs);
        Task<bool> IsFeatureIdExist(long id, long jobId);
        Task<PagingData<List<TopFeatureJobAggregates>>> SearchingFeatureJob(SearchJobWithServiceParameters parameter);
    }
}
