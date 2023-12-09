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
    public interface IBenefitRepository : IRepositoryBaseAsync<Benefit, int, JobiContext>
    {
        
        /// Description : Kiểm tra tên đã tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsExistedAsync(int id, string name);
    }
}
