using BestCV.Core.Repositories;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BestCV.Infrastructure.Repositories.Implement
{
    public class OrderStatusRepository : RepositoryBaseAsync<OrderStatus, int, JobiContext>, IOrderStatusRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public OrderStatusRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            this.db = db;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">OrderStatusId</param>
        /// <param name="color">OrderStatusColor</param>
        /// <returns>bool</returns>
        public async Task<bool> IsColorExistAsync(int id, string color)
        {
            return await db.OrderStatuses.AnyAsync(c => c.Color.ToLower().Trim() == color.ToLower().Trim() && c.Id != id && c.Active);

        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 26/07/2023
        /// Description: check name is exist
        /// </summary>
        /// <param name="id">OrderStatusId</param>
        /// <param name="name">OrderStatusName</param>
        /// <returns>bool</returns>
        public async Task<bool> IsNameExistAsync(int id, string name)
        {
            return await db.OrderStatuses.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id && c.Active);

        }
    }

}
