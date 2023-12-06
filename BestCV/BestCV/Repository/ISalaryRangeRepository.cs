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
    public interface ISalaryRangeRepository : IRepositoryBaseAsync<SalaryRange, int, JobiContext>
    {
        /// <summary>
        /// Author ThanhND
        /// CreatedTime : 02/08/2023
        /// Description : Return true if existed, opposite false
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsSalaryRangeExistAsync(string name, int id);

    }
}
