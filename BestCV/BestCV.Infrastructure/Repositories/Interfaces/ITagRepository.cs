using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Tag;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ITagRepository : IRepositoryBaseAsync<Tag, int, JobiContext>
    {
        /// <summary>
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">TagId</param>
        /// <param name="name">TagName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameTypePostExistAsync(int id, string name);

        /// <summary>
        /// Description: check name is exist for job type
        /// </summary>
        /// <param name="id">TagId</param>
        /// <param name="name">TagName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameTypeJobExistAsync(int id, string name);

        /// <summary>
        /// Description: list tag
        /// </summary>
        /// <param name="obj">TagForSelect2Aggregates</param>
        /// <returns>object</returns>
        Task<Object> ListSelectTagAsync(TagForSelect2Aggregates obj);

        /// <summary>
        /// Description: list tag
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<object> ListTagAggregatesAsync(DTParameters parameters);

        /// <summary>
        /// description: list tag type job
        /// </summary>
        /// <returns></returns>
        Task<List<Tag>> ListTagTypeJob();

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsNameExisAsync(string name, int id);
    }
}
