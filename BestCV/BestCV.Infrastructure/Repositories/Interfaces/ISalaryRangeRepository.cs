using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ISalaryRangeRepository : IRepositoryBaseAsync<SalaryRange, int, JobiContext>
    {
        /// <summary>
        /// Description : Return true if existed, opposite false
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsSalaryRangeExistAsync(string name, int id);

    }
}
