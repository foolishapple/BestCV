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
    public interface IVoucherTypeRepository : IRepositoryBaseAsync<VoucherType, int, JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Check voucher type name is existed
        /// </summary>
        /// <param name="name">voucher type name</param>
        /// <param name="id">voucher type id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
    }
}
