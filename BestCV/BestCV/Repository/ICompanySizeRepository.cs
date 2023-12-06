using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICompanySizeRepository : IRepositoryBaseAsync<CompanySize, int, JobiContext>
    {
        /// <summary>
        /// Author ThanhND
        /// CreatedTime : 07/08/2023
        /// Description : Return true if existed, opposite false
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsCompanySizeExistAsync(string name, int id);

        /// <summary>
        /// Author : HuyDQ
        /// CreatedTime : 17/08/2023
        /// Description : lấy ra thông tin của quy mô công ty và số lượng công ty thuộc quy mô này
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public Task<object> LoadDataFilterCompanySizeHomePageAsync();
    }
}
