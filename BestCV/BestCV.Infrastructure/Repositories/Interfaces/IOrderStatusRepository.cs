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
    public interface IOrderStatusRepository : IRepositoryBaseAsync<OrderStatus, int, JobiContext>
    {
        /// <summary>
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">OrderStatusId</param>
        /// <param name="name">OrderStatusName</param>
        /// <returns>bool</returns>
        Task<bool> IsNameExistAsync(int id, string name);

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check color is exist
        /// </summary>
        /// <param name="id">OrderStatusId</param>
        /// <param name="color">OrderStatusColor</param>
        /// <returns>bool</returns>
        Task<bool> IsColorExistAsync(int id, string color);

    }
}
