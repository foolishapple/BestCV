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
    public interface IOrderTypeRepository : IRepositoryBaseAsync<OrderType, int, JobiContext>
    {
        /// <summary>
        /// Description: Check order type name is existed
        /// </summary>
        /// <param name="name">order type name</param>
        /// <param name="id">order type id</param>
        /// <returns></returns>
        Task<bool> NameIsExisted(string name, int id);
    }
}
