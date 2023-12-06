using Jobi.Core.Repositories;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Jobi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Implement
{
    public class SystemConfigRepository : RepositoryBaseAsync<SystemConfig,int, JobiContext>, ISystemConfigRepository
    {
        private readonly JobiContext _db;
        private readonly IUnitOfWork<JobiContext> _unitOfWork;
        public SystemConfigRepository(JobiContext db, IUnitOfWork<JobiContext> unitOfWork): base(db,unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Author: TUNGTD
        /// Createad: 08/08/2023
        /// Description: Check system config key is existed
        /// </summary>
        /// <param name="key">system config key</param>
        /// <param name="id">system config id</param>
        /// <returns></returns>
        public async Task<bool> KeyIsExisted(string key, int id)
        {
            return await _db.SystemConfigs.AnyAsync(c => c.Key == key && c.Active && c.Id != id);
        }
    }
}
