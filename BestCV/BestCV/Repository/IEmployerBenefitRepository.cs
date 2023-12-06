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
    public interface IEmployerBenefitRepository : IRepositoryBaseAsync<EmployerBenefit, int, JobiContext>
    {
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// Description : Kiểm tra tên đã tồn tại
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsExistedAsync(int id, string name);
    }
}
