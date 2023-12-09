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
    public class AccountStatusRepository : RepositoryBaseAsync<AccountStatus, int, JobiContext>, IAccountStatusRepository
    {
        private readonly JobiContext db;
        private readonly IUnitOfWork<JobiContext> unitOfWork;
        public AccountStatusRepository(JobiContext _dbContext, IUnitOfWork<JobiContext> _unitOfWork) : base(_dbContext, _unitOfWork)
        {
            db = _dbContext;
            unitOfWork = _unitOfWork;
        }
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 01/08/2023
        /// </summary>
        /// <param name="name">accountStatusName</param>
        /// <param name="id">accountStatusId</param>
        /// <returns>True if exist, opposite false</returns>
        public async Task<bool> IsAccountStatusExistAsync(string name, int id)
        {
            return await db.AccountStatuses.AnyAsync(c => c.Active && c.Name.ToLower().Trim() == name.ToLower().Trim() && c.Id != id);
        }
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 01/08/2023
        /// </summary>
        /// <param name="name">accountStatusColor</param>
        /// <param name="id">accountStatusId</param>
        /// <returns>True if exist, opposite false</returns>
        public async Task<bool> IsColorExistAsync(string color, int id)
        {
            return await db.AccountStatuses.AnyAsync(c => c.Active && c.Color.ToLower().Trim() == color.ToLower().Trim() && c.Id != id);
        }
    }
}
