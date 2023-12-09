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
    public interface IVoucherTypeRepository : IRepositoryBaseAsync<VoucherType, int, JobiContext>
    {
        /// <summary>
        /// Description: Check voucher type name is existed
        /// </summary>
        /// <param name="name">voucher type name</param>
        /// <param name="id">voucher type id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
    }
}
