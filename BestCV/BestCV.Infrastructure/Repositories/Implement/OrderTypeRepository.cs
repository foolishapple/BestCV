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

namespace BestCV.Infrastructure.Repositories.Implement
{
    internal class OrderTypeRepository : RepositoryBaseAsync<OrderType, int, JobiContext>, IOrderTypeRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public OrderTypeRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork) : base(db, unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check order type name is existed
        /// </summary>
        /// <param name="name">order type name</param>
        /// <param name="id">order type id</param>
        /// <returns></returns>
        public async Task<bool> NameIsExisted(string name, int id)
        {
            return await _db.OrderTypes.AnyAsync(c => c.Name == name && c.Active && c.Id != id);
        }
    }
}
