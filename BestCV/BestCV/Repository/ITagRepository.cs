using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.Tag;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ITagRepository : IRepositoryBaseAsync<Tag, int, JobiContext>
    {
        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">TagId</param>
        /// <param name="name">TagName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameTypePostExistAsync(int id, string name);

        /// <summary>
        /// Author: truongthieuhuyen
        /// CreatedAt: 18/08/2023
        /// Description: check name is exist for job type
        /// </summary>
        /// <param name="id">TagId</param>
        /// <param name="name">TagName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameTypeJobExistAsync(int id, string name);

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: list tag
        /// </summary>
        /// <param name="obj">TagForSelect2Aggregates</param>
        /// <returns>object</returns>
        Task<Object> ListSelectTagAsync(TagForSelect2Aggregates obj);

        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 13/09/2023
        /// Description: list tag
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<object> ListTagAggregatesAsync(DTParameters parameters);

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 18.08.2023
        /// description: list tag type job
        /// </summary>
        /// <returns></returns>
        Task<List<Tag>> ListTagTypeJob();

        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedAt: 18/09/2023
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsNameExisAsync(string name, int id);
    }
}
